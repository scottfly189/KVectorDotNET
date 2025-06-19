// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.IPlugins
{
    public abstract class BasePlugin : IPlugin
    {
        /// <summary>
        /// 在启用插件之后: 这时插件Assemblies已被加载(插件上下文已load)
        /// </summary>
        /// <returns>
        /// <para>当 IsSuccess 为 False, 主程序之后会回滚插件状态: (1)unload插件上下文 (2)更新plugin.config.json,标记为禁用状态</para>
        /// </returns>
        public virtual (bool IsSuccess, string Message) AfterEnable()
        {
            return (true, "启用成功");
        }

        /// <summary>
        /// 在禁用插件之前: 这时插件Assemblies还未被释放(插件上下文未Unload)
        /// </summary>
        /// <returns>
        /// <para>只有当 IsSuccess 为 True, 主程序之后才会释放插件上下文, 并标记为已禁用</para>
        /// <para>当 IsSuccess 为 False, 主程序不会释放插件上下文，也不会标记为禁用, 插件维持原状态</para>
        /// </returns>
        public virtual (bool IsSuccess, string Message) BeforeDisable()
        {
            return (true, "禁用成功");
        }

        /// <summary>
        /// TODO: 更新未做
        /// </summary>
        /// <param name="currentVersion"></param>
        /// <param name="targetVersion"></param>
        /// <returns></returns>
        public virtual (bool IsSuccess, string Message) Update(string currentVersion, string targetVersion)
        {
            return (true, "更新成功");
        }

        public virtual void AppStart() 
        {
            
        }

        public virtual List<string> AppStartOrderDependPlugins
        {
            get
            {
                return new List<string>();
            }
        }
    }
}


