// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.IO;
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
    /// TODO: 未测试
    /// </summary>
    public class PluginContentFilterMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IPluginFinder _pluginFinder;

        public PluginContentFilterMiddleware(
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
            await RequestBodyFilter(httpContext);

            // Call the next delegate/middleware in the pipeline
            await _next(httpContext);

            if (httpMethod == "GET" && path.EndsWith(".css") || path.EndsWith(".js") || path.EndsWith(".html"))
            {
                // middleware 回退时 过滤
                await ResponseBodyFilter(httpContext);
            }
        }

        private async Task RequestBodyFilter(HttpContext httpContext)
        {
            string content = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                // Response.Body
                await httpContext.Request.Body.CopyToAsync(memoryStream);

                long pos = httpContext.Request.Body.Position;

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    content = await reader.ReadToEndAsync();
                }
            }

            var plugins = this._pluginFinder.EnablePlugins<PluginCore.IPlugins.IContentFilterPlugin>().ToList();

            foreach (var item in plugins)
            {
                // 调用
                content = await item?.RequestBodyFilter(httpContext.Request.Path.Value, content);
            }

            // 更新 Request.Body

            #region 方式1
            //var requestStream = new MemoryStream();
            //using (StreamWriter writer = new StreamWriter(requestStream, Encoding.UTF8))
            //{
            //    await writer.WriteAsync(content);
            //}
            //httpContext.Request.Body = requestStream;
            #endregion


            #region 方式2
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            await httpContext.Request.Body.WriteAsync(buffer, 0, buffer.Length);
            #endregion
        }

        private async Task ResponseBodyFilter(HttpContext httpContext)
        {
            string content = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                // Response.Body
                await httpContext.Response.Body.CopyToAsync(memoryStream);

                long pos = httpContext.Response.Body.Position;

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    content = await reader.ReadToEndAsync();
                }
            }

            var plugins = this._pluginFinder.EnablePlugins<PluginCore.IPlugins.IContentFilterPlugin>().ToList();

            foreach (var item in plugins)
            {
                // 调用
                content = await item?.ReponseBodyFilter(httpContext.Request.Path.Value, content);
            }

            // 更新 ResponseBody

            #region 方式1
            //var responseStream = new MemoryStream();
            //using (StreamWriter writer = new StreamWriter(responseStream, Encoding.UTF8))
            //{
            //    await writer.WriteAsync(content);
            //}
            //httpContext.Response.Body = responseStream;
            #endregion


            #region 方式2
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            await httpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            #endregion
        }

    }
}


