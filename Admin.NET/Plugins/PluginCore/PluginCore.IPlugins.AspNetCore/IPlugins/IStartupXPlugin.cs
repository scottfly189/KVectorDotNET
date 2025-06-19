// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PluginCore.IPlugins
{
    /// <summary>
    /// 实验阶段
    /// <para>热插拔: 已有效化</para>
    /// </summary>
    public interface IStartupXPlugin : IPlugin
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// <para>未有效化</para>
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);


        int ConfigureServicesOrder { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void Configure(IApplicationBuilder app);

        int ConfigureOrder { get; }
    }
}


