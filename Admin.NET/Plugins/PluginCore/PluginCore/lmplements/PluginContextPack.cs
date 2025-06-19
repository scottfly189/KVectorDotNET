// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginCore.lmplements
{
    public class PluginContextPack : IPluginContextPack
    {
        public IPluginContext Pack(string pluginId)
        {
            #region 加载插件主dll

            // 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, $"{pluginId}.dll");
            // 此插件的 加载上下文
            var context = new PluginLoadContext(pluginId, pluginMainDllFilePath);
            Assembly pluginMainAssembly;
            // 微软文档推荐 LoadFromAssemblyName
            pluginMainAssembly = context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginMainDllFilePath)));

            #region 第2种方法: 未在这种情况下测试
            //using (var fs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            //{
            //    // 使用此方法, 就不会导致dll被锁定
            //    pluginMainAssembly = context.LoadFromStream(fs);

            //    // 加载其中的控制器
            //    _pluginControllerManager.AddControllers(pluginMainAssembly);
            //} 
            #endregion

            #endregion


            return context;
        }
    }
}


