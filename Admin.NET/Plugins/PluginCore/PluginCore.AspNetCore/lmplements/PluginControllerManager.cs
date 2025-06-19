// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using Microsoft.AspNetCore.Mvc.ApplicationParts;
using PluginCore.AspNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PluginCore.AspNetCore.lmplements
{
    public class PluginControllerManager : IPluginControllerManager
    {
        private readonly ApplicationPartManager _applicationPartManager;

        public PluginControllerManager(ApplicationPartManager applicationPartManager)
        {
            _applicationPartManager = applicationPartManager;
        }

        /// <summary>
        /// 从指定<see cref="Assembly"/> 中获取 Controller, 并添加进来
        /// </summary>
        /// <param name="assembly"></param>
        public void AddControllers(Assembly assembly)
        {
            AssemblyPart assemblyPart = new AssemblyPart(assembly);
            _applicationPartManager.ApplicationParts.Add(assemblyPart);

            ResetControllActions();
        }

        public void RemoveControllers(string pluginId)
        {
            ApplicationPart last = _applicationPartManager.ApplicationParts.First(m => m.Name == pluginId);
            _applicationPartManager.ApplicationParts.Remove(last);

            ResetControllActions();
        }

        /// <summary>
        /// 通知应用(主程序)Controller.Action 已发生变化
        /// </summary>
        private void ResetControllActions()
        {
            PluginActionDescriptorChangeProvider.Instance.HasChanged = true;
            // TokenSource 为 null
            // 注意: 在程序刚启动时, 未抵达 Controller 时不会触发 IActionDescriptorChangeProvider.GetChangeToken(), 也就会导致 TokenSource 为 null, 在启动时，同时在启动时，插件Controller.Action和主程序一起被添加，此时无需通知改变
            if (PluginActionDescriptorChangeProvider.Instance.TokenSource != null)
            {
                // 只有在插件列表控制启用，禁用时才需通知改变
                PluginActionDescriptorChangeProvider.Instance.TokenSource.Cancel();
            }
        }
    }
}


