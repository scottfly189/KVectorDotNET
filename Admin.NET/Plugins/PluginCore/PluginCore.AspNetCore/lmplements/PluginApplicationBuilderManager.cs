// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.Interfaces;
using PluginCore.IPlugins;
using PluginCore.AspNetCore.Middlewares;
using PluginCore.AspNetCore.Infrastructure;

namespace PluginCore.AspNetCore.lmplements
{
    public class PluginApplicationBuilderManager : PluginApplicationBuilderManager<PluginApplicationBuilder>
    {
        public PluginApplicationBuilderManager(IPluginFinder pluginFinder) : base(pluginFinder)
        {
        }
    }

    public class PluginApplicationBuilderManager<TPluginApplicationBuilder> : IPluginApplicationBuilderManager
        where TPluginApplicationBuilder : PluginApplicationBuilder, new()
    {
        private readonly IPluginFinder _pluginFinder;

        public PluginApplicationBuilderManager(IPluginFinder pluginFinder)
        {
            _pluginFinder = pluginFinder;
        }

        public static RequestDelegate RequestDelegateResult { get; set; }


        /// <summary>
        /// 插件 启用, 禁用 时: 重新 Build
        /// </summary>
        public void ReBuild()
        {
            TPluginApplicationBuilder applicationBuilder = new TPluginApplicationBuilder();
            applicationBuilder.ReachEndAction = PluginStartupXMiddleware.ReachedEndAction;

            var plugins = this._pluginFinder.EnablePlugins<IStartupXPlugin>()?.OrderBy(m => m.ConfigureOrder)?.ToList();
            foreach (var item in plugins)
            {
                // 调用
                Utils.LogUtil.Info<PluginApplicationBuilderManager>($"{item.GetType().ToString()} {nameof(IStartupXPlugin)}.{nameof(IStartupXPlugin.Configure)}");

                item.Configure(applicationBuilder);
            }

            RequestDelegateResult = applicationBuilder.Build();
        }


        public RequestDelegate GetBuildResult()
        {
            if (RequestDelegateResult == null)
            {
                ReBuild();
            }

            return RequestDelegateResult;
        }

    }
}


