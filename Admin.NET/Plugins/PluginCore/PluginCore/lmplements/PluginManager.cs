// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    /// <summary>
    /// 一个插件的所有dll由 一个 <see cref="IPluginContext"/> 管理
    /// <see cref="PluginContextManager"/> 记录管理了所有 插件的<see cref="IPluginContext"/>
    /// <see cref="PluginManager"/> 是对 <see cref="PluginContextManager"/>的封装, 使其更好管理插件加载释放的行为
    /// </summary>
    public class PluginManager : IPluginManager
    {
        public IPluginContextManager PluginContextManager { get; set; }

        public IPluginContextPack PluginContextPack { get; set; }

        public PluginManager(IPluginContextManager pluginContextManager, IPluginContextPack pluginContextPack)
        {
            this.PluginContextManager = pluginContextManager;
            this.PluginContextPack = pluginContextPack;
        }

        /// <summary>
        /// 加载插件程序集
        /// </summary>
        /// <param name="pluginId"></param>
        public void LoadPlugin(string pluginId)
        {
            IPluginContext context = this.PluginContextPack.Pack(pluginId);

            // 这个插件加载上下文 放入 集合中
            this.PluginContextManager.Add(pluginId, context);
        }

        public void UnloadPlugin(string pluginId)
        {
            this.PluginContextManager.Remove(pluginId);
        }
        public Assembly GetPluginAssembly(string pluginId)
        {
            IPluginContext context = this.PluginContextPack.Pack(pluginId);
            Assembly pluginMainAssembly = context.LoadFromAssemblyName(new AssemblyName(pluginId));

            return pluginMainAssembly;
        }
    }
}


