// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using PluginCore.Interfaces;
using PluginCore.IPlugins;
using PluginCore.Utils;

namespace PluginCore.AspNetCore.BackgroundServices
{
    public class PluginTimeJobBackgroundService : TimeBackgroundService
    {
        /// <summary>
        /// 插件与之最近执行时间
        /// <para>最近执行时间: 10位秒 时间戳</para>
        /// </summary>
        private readonly Dictionary<string, long> _pluginAndLastExecuteTimeDic = new Dictionary<string, long>();

        private readonly IPluginFinder _pluginFinder;

        private static readonly object _doWorklock = new object();

        public PluginTimeJobBackgroundService(IPluginFinder pluginFinder)
        {
            _pluginFinder = pluginFinder;
            // 最小间隔 1 秒
            _timerPeriod = TimeSpan.FromSeconds(1);
        }

        protected override void DoWork(object state)
        {
            lock (_doWorklock)
            {
                //Console.WriteLine("Memory used before collection: {0:N0}", GC.GetTotalMemory(false));

                var plugins = this._pluginFinder.EnablePlugins<ITimeJobPlugin>().ToList();

                List<string> enabledPluginKeyList = new List<string>();
                foreach (var item in plugins)
                {
                    string pluginKey = item.GetType().ToString();
                    enabledPluginKeyList.Add(pluginKey);
                    if (this._pluginAndLastExecuteTimeDic.ContainsKey(pluginKey))
                    {
                        long lastExecuteTime = this._pluginAndLastExecuteTimeDic[pluginKey];
                        long nowTime = DateTime.Now.ToTimeStamp10();
                        if (nowTime - lastExecuteTime >= item.SecondsPeriod)
                        {
                            // 调用
                            Utils.LogUtil.Info<PluginTimeJobBackgroundService>($"{pluginKey}: {nameof(ITimeJobPlugin)}.{nameof(ITimeJobPlugin.ExecuteAsync)}");
                            Task task = item?.ExecuteAsync();
                            this._pluginAndLastExecuteTimeDic[pluginKey] = DateTime.Now.ToTimeStamp10();
                        }
                    }
                    else
                    {
                        // 调用
                        Utils.LogUtil.Info<PluginTimeJobBackgroundService>($"{pluginKey}: {nameof(ITimeJobPlugin)}.{nameof(ITimeJobPlugin.ExecuteAsync)}");
                        Task task = item?.ExecuteAsync();
                        this._pluginAndLastExecuteTimeDic.Add(pluginKey, DateTime.Now.ToTimeStamp10());
                    }
                }
                // 所有插件遍历结束
                // 出现在了 _pluginAndLastExecuteTimeDic 中，但没有出现在 enabledPluginKeyList, 说明为之前启用过，但现在已禁用的插件，需要去除掉
                List<string> keys = this._pluginAndLastExecuteTimeDic.Select(m => m.Key).ToList();
                foreach (string key in keys)
                {
                    if (!enabledPluginKeyList.Contains(key))
                    {
                        this._pluginAndLastExecuteTimeDic.Remove(key);
                    }
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                //Console.WriteLine("Memory used after full collection: {0:N0}", GC.GetTotalMemory(true));
            }
        }
    }
}


