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
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace PluginCore.AspNetCore.AdminUI
{
    public static class PluginCoreAdminUIBuilderExtensions
    {

        /// <summary>
        /// Register the SwaggerUI middleware with provided options
        /// </summary>
        public static IApplicationBuilder UsePluginCoreAdminUI(this IApplicationBuilder app, PluginCoreAdminUIOptions options)
        {
            #region Old - 区分
            //Config.PluginCoreConfig pluginCoreConfig = Config.PluginCoreConfigFactory.Create();

            //switch (pluginCoreConfig.FrontendMode?.ToLower())
            //{
            //    case "localembedded":
            //        app.UseMiddleware<PluginCoreAdminUIMiddleware>(options);
            //        break;
            //    case "localfolder":

            //        #region LocalFolder
            //        //string contentRootPath = Directory.GetCurrentDirectory();

            //        // https://docs.microsoft.com/zh-CN/aspnet/core/fundamentals/static-files?view=aspnetcore-5.0
            //        //var options = new DefaultFilesOptions()
            //        //{
            //        //    RequestPath = "/PluginCore/Admin",
            //        //};
            //        //// TODO: 404: 无效, 失败, 改为使用 Controller 手动指定
            //        ////options.DefaultFileNames.Add("PluginCoreAdmin/index.html");
            //        //app.UseDefaultFiles(options);

            //        // 注意: 为了无需重启Web，而更新是否本地前端配置, 因此此项保持常驻开启
            //        // 因此, 需要保证 PluginCoreAdmin 文件夹存在
            //        string pluginCoreAdminDir = PluginPathProvider.PluginCoreAdminDir();
            //        app.UseStaticFiles(new StaticFileOptions
            //        {
            //            FileProvider = new PhysicalFileProvider(
            //                 pluginCoreAdminDir),
            //            RequestPath = "/PluginCore/Admin"
            //        });
            //        #endregion

            //        break;
            //    case "remotecdn":

            //        break;
            //    default:
            //        app.UseMiddleware<PluginCoreAdminUIMiddleware>(options);
            //        break;
            //}

            //return app; 
            #endregion

            
            return app.UseMiddleware<PluginCoreAdminUIMiddleware>(options);
        }

        /// <summary>
        /// Register the SwaggerUI middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UsePluginCoreAdminUI(
            this IApplicationBuilder app)
        {
            PluginCoreAdminUIOptions options = new PluginCoreAdminUIOptions()
            {

            };

            return app.UsePluginCoreAdminUI(options);
        }


    }
}


