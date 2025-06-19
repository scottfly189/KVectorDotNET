// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using PluginCore.IPlugins;

namespace PluginCore.lmplements
{

    /// <summary>
    /// 插件发现者: 找启用的插件(1.plugin.config.json中启用 2. 有插件上下文)
    /// TODO: 其实是没必要再效验plugin.config.json的，因为只有启用的插件才有上下文, 为了保险，暂时这么做
    /// 注意: 这意味着一个启用的插件需同时满足这两个条件
    /// </summary>
    /// <remarks>
    /// 依赖解析: IServiceScopeFactory
    /// </remarks>
    public class PluginFinder : PluginFinderV2
    {
        public PluginFinder(IPluginContextManager pluginContextManager, IServiceScopeFactory serviceScopeFactory) : base(pluginContextManager, serviceScopeFactory)
        {
        }
    }
}


