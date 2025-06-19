// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.Infrastructure;

namespace PluginCore.AspNetCore.Middlewares
{
    public class PluginStartupXMiddleware
    {
        private readonly RequestDelegate _next;

        public PluginStartupXMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public static Action ReachedEndAction { get; set; } = () => { _isReachedEnd = true; };

        private static bool _isReachedEnd;

        public async Task InvokeAsync(HttpContext httpContext, IPluginApplicationBuilderManager pluginApplicationBuilderManager)
        {
            //bool isReachedEnd = false;
            _isReachedEnd = false;

            try
            {
                RequestDelegate requestDelegate = pluginApplicationBuilderManager.GetBuildResult();

                await requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                // InvalidOperationException: The request reached the end of the pipeline without executing the endpoint: 'AspNetCore3_1.Controllers.WeatherForecastController.Get (AspNetCore3_1)'. Please register the EndpointMiddleware using 'IApplicationBuilder.UseEndpoints(...)' if using routing.
                Utils.LogUtil.Error<PluginStartupXMiddleware>(ex, ex.Message);
                if (ex.InnerException != null)
                {
                    Utils.LogUtil.Error<PluginStartupXMiddleware>(ex.InnerException, ex.InnerException.Message);
                }
            }

            if (_isReachedEnd)
            {
                // Call the next delegate/middleware in the pipeline
                await _next(httpContext);
            }
            else
            {
                // 没有抵达 End, 说明在插件的 middleware 中已堵塞, 准备返回 响应
            }


        }

    }
}


