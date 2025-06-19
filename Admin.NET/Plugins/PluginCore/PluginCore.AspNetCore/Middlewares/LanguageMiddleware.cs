// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluginCore.IPlugins;

namespace PluginCore.AspNetCore.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // 从 Cookie 中获取语言标识
            string language = httpContext.Request.Cookies[Constants.AspNetCoreLanguageCookieName];

            // 存储当前语言到 HttpContext.Items
            httpContext.Items[Constants.AspNetCoreLanguageKey] = language;

            // 调用下一个中间件
            await _next(httpContext);
        }
    }

    public static class LanguageMiddlewareExtensions
    {
        public static IApplicationBuilder UsePluginCoreLanguageMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LanguageMiddleware>();
        }
    }
}
