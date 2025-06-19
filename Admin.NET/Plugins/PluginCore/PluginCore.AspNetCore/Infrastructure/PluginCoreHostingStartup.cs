// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PluginCore.AspNetCore.Extensions;

[assembly: HostingStartup(typeof(PluginCore.AspNetCore.Infrastructure.PluginCoreHostingStartup))]
namespace PluginCore.AspNetCore.Infrastructure
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/platform-specific-configuration?view=aspnetcore-5.0
    /// </summary>
    public class PluginCoreHostingStartup : IHostingStartup
    {
        public PluginCoreHostingStartup()
        {

        }

        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureAppConfiguration(config =>
            //{

            //});

            // 注意: 无论是通过 Program.cs 中 webBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "PluginCore");
            // 还是 通过环境变量 指定承载启动程序集, 都是先执行 外部的承载启动程序集, 再执行主程序的 Startup.cs, 因此在这时, 有些 service 还没有注册

            // TODO: 不知道, 重复 Add, Use 会导致什么, 没有做防止重复
            builder.ConfigureServices(services =>
            {
                // fixed: https://github.com/yiyungent/PluginCore/issues/1
                // System.InvalidOperationException: 'Unable to resolve service for type 'Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager'
                // TODO: 不确定, 这样是否可行, 事实上之后主程序还会 Add 一次, 不知道是否会导致存在多个 实例
                // 失败: 不是一个实例, 导致无法改变 Controller
                //Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager applicationPartManager =
                //    new Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager();
                //services.AddSingleton<Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPartManager>(applicationPartManager);

                services.AddPluginCore();
            });

            builder.Configure(app =>
            {
                app.UsePluginCore();
            });
        }
    }
}


