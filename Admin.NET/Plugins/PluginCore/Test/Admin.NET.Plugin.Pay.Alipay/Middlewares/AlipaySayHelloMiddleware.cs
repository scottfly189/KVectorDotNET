// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using Admin.NET.Plugin.Core.Abstractions.Pay;
using Microsoft.AspNetCore.Http;
using PluginCore.Interfaces;
using PluginCore.IPlugins;
using System.Text;

namespace Admin.NET.Plugin.Pay.Alipay.Middlewares
{
    public class AlipaySayHelloMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 在 <see cref="PluginApplicationBuilder"/> Build 时, 将会 new Middleware(), 最终将所有 Middleware 包装为一个 <see cref="RequestDelegate"/>
        /// </summary>
        /// <param name="next"></param>
        public AlipaySayHelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="pluginFinder">测试，是否运行时添加的Middleware，是否可以依赖注入</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext, IPluginFinder pluginFinder)
        {
            // 测试: 成功
            List<IPlugin> plugins = pluginFinder.EnablePlugins()?.ToList();
            // 所有实现了 ITestPlugin 的已启用插件
            var payPlugins = pluginFinder.EnablePlugins<IPayPlugin>().ToList();
            bool isMatch = false;

            isMatch = httpContext.Request.Path.Value.StartsWith("/SayHello");

            if (isMatch)
            {
                string sayhello = payPlugins[0].Say();
                await httpContext.Response.WriteAsync($"Hello World! {DateTime.Now:yyyy-MM-dd HH:mm:ss} <br>" +
                    $"Say Hello! {sayhello} <br>" +
                                                      $"{httpContext.Request.Path} <br>" +
                                                      $"{httpContext.Request.QueryString.Value}", Encoding.UTF8);
            }
            else
            {
                // Call the next delegate/middleware in the pipeline
                await _next(httpContext);
            }
        }

    }
}
