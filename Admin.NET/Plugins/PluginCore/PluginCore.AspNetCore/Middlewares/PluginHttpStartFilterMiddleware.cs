// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PluginCore.Interfaces;

namespace PluginCore.AspNetCore.Middlewares
{
    /// <summary>
    /// 一定在 PluginCore 添加的中间件中 第一个
    /// </summary>
    public class PluginHttpStartFilterMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IPluginFinder _pluginFinder;

        public PluginHttpStartFilterMiddleware(
            RequestDelegate next,
            IWebHostEnvironment hostingEnv,
            ILoggerFactory loggerFactory,
            IPluginFinder pluginFinder)
        {
            _next = next;
            _pluginFinder = pluginFinder;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path.Value;

            // 在请求下一个 middleware 前过滤
            await Filter(httpContext);

            // Call the next delegate/middleware in the pipeline
            await _next(httpContext);

            // middleware 回退时 过滤
        }

        private async Task Filter(HttpContext httpContext)
        {
            var plugins = this._pluginFinder.EnablePlugins<PluginCore.IPlugins.IHttpFilterPlugin>().ToList();

            foreach (var item in plugins)
            {
                // 调用
                await item?.HttpStartFilter(httpContext);
            }
        }



    }
}


