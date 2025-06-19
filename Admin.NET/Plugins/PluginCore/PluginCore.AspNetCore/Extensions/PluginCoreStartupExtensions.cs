// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using PluginCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using PluginCore.AspNetCore.Authorization;
using PluginCore.AspNetCore.AdminUI;
using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using PluginCore.AspNetCore.Middlewares;
using PluginCore.AspNetCore.BackgroundServices;
using PluginCore.IPlugins;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.lmplements;
using PluginCore.AspNetCore.lmplements;

namespace PluginCore.AspNetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PluginCoreStartupExtensions
    {
        private static IWebHostEnvironment _webHostEnvironment;

        private static IServiceCollection _services;

        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// 若需要替换默认实现, 请在 <see cref="AddPluginCore(IServiceCollection)"/> 之前, 若在之后, 则无法影响 主程序启动时默认行为
        /// </summary>
        /// <param name="services"></param>
        public static void AddPluginCore(this IServiceCollection services)
        {
            services.AddPluginCoreServices();

            services.AddPluginCoreLog();

            services.AddPluginCorePlugins();

            #region PluginCore Admin 权限
            // 1. 先 Authentication (我是谁) 2. 再 Authorization (我能做什么)

            // Authentication
            //注销内置认证 by tomny 
            //  services.AddPluginCoreAuthentication();
            //注销内置授权 by tomny 
            // Authorization
            //services.AddPluginCoreAuthorization();
            #endregion

            services.AddPluginCoreStartupPlugin();

            // AddBackgroundServices
            services.AddBackgroundServices();

            // 一定要在最后
            _services = services;
            _serviceProvider = services.BuildServiceProvider();
        }

        public static IApplicationBuilder UsePluginCore(this IApplicationBuilder app)
        {
            // 一定在 PluginCore 添加的中间件中 第一个
            app.UseMiddleware<PluginHttpStartFilterMiddleware>();

            app.UsePluginCoreLanguageMiddleware();
            //注销内置管理页面 by tomny 
           // app.UsePluginCoreAdminUI();

            app.UsePluginCoreStaticFiles();

            // 发现 UseAuthentication 认证中间件重复添加, 也只会执行一次 认证
            // 但 UseAuthorization 重复添加2次, 则会执行 2次 授权
            //注销内置认证授权 by tomny 
            //app.UseAuthentication();
            //app.UseAuthorization();

            #region Plugin Middleware
            // Plugin Middleware
            //app.UseMiddleware<PluginContentFilterMiddleware>();


            // 一定在 PluginCore 添加的中间件中 最后一个
            app.UseMiddleware<PluginHttpEndFilterMiddleware>();
            #endregion

            app.UsePluginCoreStartupPlugin();

            app.UsePluginCoreAppStart();

            // Log
            app.UsePluginCoreLog();

            return app;
        }


        public static void AddPluginCoreServices(this IServiceCollection services)
        {
            #region 注册服务

            #region 仅适用于 ASP.NET Core
            // start: 仅用于 ASP.NET Core
            // 用于添加插件Controller 时，通知Controller.Action发生变化
            services.AddSingleton<IActionDescriptorChangeProvider>(PluginActionDescriptorChangeProvider.Instance);
            services.AddSingleton(PluginActionDescriptorChangeProvider.Instance);

            services.TryAddTransient<PluginControllerManager>();
            services.TryAddTransient<IPluginControllerManager, PluginControllerManager>();

            services.TryAddTransient<PluginApplicationBuilderManager>();
            services.TryAddTransient<IPluginApplicationBuilderManager, PluginApplicationBuilderManager>();
            // end: 仅用于 ASP.NET Core 
            #endregion

            #region 通用

            // v1 旧版
            //services.TryAddTransient<PluginContextPackV1>();
            //services.TryAddTransient<IPluginContextPack, PluginContextPackV1>();
            //services.TryAddTransient<AspNetCorePluginManagerV1>();
            //services.TryAddTransient<IPluginManager, AspNetCorePluginManagerV1>();

            services.TryAddTransient<PluginContextPack>();
            services.TryAddTransient<IPluginContextPack, PluginContextPack>();
            //注销内置插件管理 by tomny 
            services.TryAddTransient<AspNetCorePluginManager>();
            services.TryAddTransient<IPluginManager, AspNetCorePluginManager>();

            services.TryAddTransient<PluginFinderV1>();
            services.TryAddTransient<PluginFinderV2>();
            services.TryAddTransient<PluginFinder>();
            services.TryAddTransient<IPluginFinder, PluginFinder>();

            // 注意: 它必须单例
            // TODO: 不知道原因, 单例失败, 每次 获取 IPluginFinder 都会获取新的 IPluginContextManager
            services.TryAddSingleton<PluginContextManager>();
            services.TryAddSingleton<IPluginContextManager, PluginContextManager>();
            #endregion 

            #endregion

            // ************************* 注意: IServiceCollection 是服务列表, 但由 IServiceProvider 来负责解析, AClass 单例 仅在 AServiceProvider 范围内
            _serviceProvider = services.BuildServiceProvider();
        }

        public static void AddPluginCorePlugins(this IServiceCollection services)
        {
            #region ASP.NET Core
            //IPluginManager pluginManager = services.BuildServiceProvider().GetService<IPluginManager>();
            IPluginManager pluginManager = _serviceProvider.GetService<IPluginManager>();

            // 初始化 PluginCore 相关目录
            PluginPathProvider.PluginsRootPath();

            // 在程序启动时加载所有 已安装并启用 的插件

            // 获取 PluginConfigModel
            #region 获取 PluginConfigModel
            PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
            #endregion

            // 已启用的插件
            #region 加载 已启用插件的Assemblies
            IList<string> enabledPluginIds = pluginConfigModel.EnabledPlugins;
            foreach (var pluginId in enabledPluginIds)
            {
                pluginManager.LoadPlugin(pluginId);
            }
            #endregion

            //注销内置Controller的注册  by tomny 
            // 将本 Assembly 内的 Controller 添加
           // var ass = Assembly.GetExecutingAssembly();
            //IPluginControllerManager pluginControllerManager = services.BuildServiceProvider().GetService<IPluginControllerManager>();
           // IPluginControllerManager pluginControllerManager = _serviceProvider.GetService<IPluginControllerManager>();
            //pluginControllerManager.AddControllers(ass);


            // IWebHostEnvironment
            _webHostEnvironment = services.BuildServiceProvider().GetService<IWebHostEnvironment>();
            #endregion
        }

        public static void AddPluginCoreAuthentication(this IServiceCollection services)
        {
            // fixed: https://github.com/yiyungent/PluginCore/issues/4
            // System.InvalidOperationException: No authenticationScheme was specified, and there was no DefaultChallengeScheme found. The default schemes can be set using either AddAuthentication(string defaultScheme) or AddAuthentication(Action<AuthenticationOptions> configureOptions).
            #region 添加认证 Authentication
            // 没通过 Authentication: 401 Unauthorized
            // services.AddAuthentication("PluginCore.Authentication")
            //     .AddScheme<Authentication.PluginCoreAuthenticationSchemeOptions,
            //         Authentication.PluginCoreAuthenticationHandler>("PluginCore.Authentication", "PluginCore.Authentication",
            //         options => { });
            // 注意: 不要设置 默认 认证名: Constants.AspNetCoreAuthenticationScheme
            // services.AddAuthentication(Constants.AspNetCoreAuthenticationScheme)
            // 默认认证名: 默认
            services.AddAuthentication()
            // 添加一个新的认证方案
                .AddScheme<Authentication.PluginCoreAuthenticationSchemeOptions, Authentication.PluginCoreAuthenticationHandler>(
                    authenticationScheme: Constants.AspNetCoreAuthenticationScheme,
                    displayName: Constants.AspNetCoreAuthenticationScheme,
                    options => { });
            #endregion
        }

        public static void AddPluginCoreAuthorization(this IServiceCollection services)
        {
            #region 添加授权策略-所有标记 [PluginCoreAdminAuthorize] 都需要权限检查
            // Only Once, Not recommend
            //services.AddSingleton<IAuthorizationHandler, PluginCoreAdminAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                // options.AddPolicy("PluginCore.Admin", policy =>
                options.AddPolicy(name: Constants.AspNetCoreAuthorizationPolicyName, policy =>
                {
                    // 无法满足 下方任何一项：HTTP 403 错误
                    // 3.需要 检查是否拥有当前请求资源的权限
                    //policy.Requirements.Add(new PluginCoreAdminRequirement());
                    policy.AuthenticationSchemes = new string[] {
                        Constants.AspNetCoreAuthenticationScheme
                    };
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(claimType: Constants.AspNetCoreAuthenticationClaimType);
                    // 必须重启才能使得更改密码生效
                    string token = AccountManager.CreateToken();
                    policy.RequireClaim(claimType: Constants.AspNetCoreAuthenticationClaimType, allowedValues: new string[] {
                        token
                    });
                    //policy.RequireAssertion(context =>
                    //{
                    //    return true;
                    //});
                });
            });
            #endregion

            // AccountManager
            services.AddTransient<AccountManager>();
            // HttpContext.Current
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddHttpContextAccessor(); 
        }

        public static void AddPluginCoreStartupPlugin(this IServiceCollection services)
        {
            //IPluginFinder pluginFinder = services.BuildServiceProvider().GetService<IPluginFinder>();
            IPluginFinder pluginFinder = _serviceProvider.GetService<IPluginFinder>();

            #region IStartupPlugin

            var plugins = pluginFinder.EnablePlugins<IStartupPlugin>()?.OrderBy(m => m.ConfigureServicesOrder)?.ToList();

            foreach (var item in plugins)
            {
                // 调用
                Utils.LogUtil.Info(
                    categoryName: $"{nameof(PluginCoreStartupExtensions)}.{nameof(AddPluginCoreStartupPlugin)}",
                    message: $"{item.GetType().ToString()}: {nameof(IStartupPlugin)}.{nameof(IStartupPlugin.ConfigureServices)}"
                );

                item?.ConfigureServices(services);
            }

            #endregion
        }

        public static void AddPluginCoreLog(this IServiceCollection services)
        {
            #region Logger

            IServiceScopeFactory serviceScopeFactory = _serviceProvider.GetService<IServiceScopeFactory>();
            Utils.LogUtil.Initialize(serviceScopeFactory);

            #endregion
        }

        public static IApplicationBuilder UsePluginCoreStaticFiles(this IApplicationBuilder app)
        {
            // TODO: 其实由于目前已实现运行时动态新增/删除 HTTP Middleware, 其实可以不用再像下方这么复制插件 wwwroot 目录到 Plugins_wwwroot/{PluginId}
            // 而是在运行时配置, 直接指向 `Plugins/{PluginId}/wwwroot`, 而无需复制/删除

            // 注意：`UseDefaultFiles`必须在`UseStaticFiles`之前进行调用。因为`DefaultFilesMiddleware`仅仅负责重写Url，实际上默认页文件，仍然是通过`StaticFilesMiddleware`来提供的。

            string pluginwwwrootDir = PluginPathProvider.PluginsWwwRootDir();

            #region 插件 wwwroot 默认页
            // 设置目录的默认页
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            // 指定默认页名称
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            // 指定请求路径
            defaultFilesOptions.RequestPath = "/Plugins";
            // 指定默认页所在的目录
            defaultFilesOptions.FileProvider = new PhysicalFileProvider(pluginwwwrootDir);
            app.UseDefaultFiles(defaultFilesOptions);
            #endregion

            #region 插件 wwwroot
            // 由于没办法在运行时, 动态 UseStaticFiles(), 因此不再为每一个插件都 UseStaticFiles(),
            // 而是统一在一个文件夹下, 插件启用时, 将插件的wwwroot复制到 Plugins_wwwroot/{PluginId}, 禁用时, 再删除
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    pluginwwwrootDir),
                RequestPath = "/Plugins"
            });
            #endregion

            return app;
        }

        public static IApplicationBuilder UsePluginCoreAppStart(this IApplicationBuilder app)
        {
            //IPluginFinder pluginFinder = _services.BuildServiceProvider().GetService<IPluginFinder>();
            IPluginFinder pluginFinder = _serviceProvider.GetService<IPluginFinder>();

            #region AppStart

            var plugins = pluginFinder.EnablePluginsFull()?.ToList();
            var dependencySorter = new PluginCore.Utils.DependencySorter<IPlugin>();
            dependencySorter.AddObjects(plugins.Select(m => m.PluginInstance).ToArray());
            foreach (var item in plugins)
            {
                var dependPlugins = plugins.Where(m => item.PluginInstance.AppStartOrderDependPlugins.Contains(m.PluginId)).Select(m => m.PluginInstance).ToArray();
                dependencySorter.SetDependencies(obj: item.PluginInstance, dependsOnObjects: dependPlugins);
            }
            var sortedPlugins = dependencySorter.Sort();
            foreach (var item in sortedPlugins)
            {
                // 调用
                //Utils.LogUtil.PluginBehavior(item, typeof(IPlugin), nameof(IPlugin.AppStart));
                Utils.LogUtil.Info(
                    categoryName: $"{nameof(PluginCoreStartupExtensions)}.{nameof(UsePluginCoreAppStart)}",
                    message: $"{item.GetType().ToString()}: {nameof(IPlugin)}.{nameof(IPlugin.AppStart)}"
                );

                item?.AppStart();
            }

            #endregion

            return app;
        }

        public static IApplicationBuilder UsePluginCoreStartupPlugin(this IApplicationBuilder app)
        {
            //IPluginFinder pluginFinder = _services.BuildServiceProvider().GetService<IPluginFinder>();
            IPluginFinder pluginFinder = _serviceProvider.GetService<IPluginFinder>();

            #region IStartupPlugin

            var startupPlugins = pluginFinder.EnablePlugins<IStartupPlugin>()?.OrderBy(m => m.ConfigureOrder)?.ToList();

            foreach (var item in startupPlugins)
            {
                // 调用
                //Utils.LogUtil.PluginBehavior(item, typeof(IStartupPlugin), nameof(IStartupPlugin.Configure));
                Utils.LogUtil.Info(
                    categoryName: $"{nameof(PluginCoreStartupExtensions)}.{nameof(UsePluginCoreStartupPlugin)}",
                    message: $"{item.GetType().ToString()}: {nameof(IStartupPlugin)}.{nameof(IStartupPlugin.Configure)}"
                );

                item?.Configure(app);
            }

            #endregion


            app.UseMiddleware<PluginStartupXMiddleware>();

            return app;
        }

        public static IApplicationBuilder UsePluginCoreLog(this IApplicationBuilder app)
        {
            #region 启动 Log
            Config.PluginCoreConfig pluginCoreConfig = Config.PluginCoreConfigFactory.Create();

            Utils.LogUtil.Info(categoryName: $"{nameof(PluginCoreStartupExtensions)}.{nameof(UsePluginCoreLog)}", $"{nameof(PluginCore.AspNetCore)}");
            Utils.LogUtil.Info(categoryName: $"{nameof(PluginCoreStartupExtensions)}.{nameof(UsePluginCoreLog)}", "Started successfully:");
            Utils.LogUtil.Info(categoryName: $"{nameof(PluginCoreStartupExtensions)}.{nameof(UsePluginCoreLog)}", $"Front-end mode: {pluginCoreConfig.FrontendMode}");
            Utils.LogUtil.Info(categoryName: $"{nameof(PluginCoreStartupExtensions)}.{nameof(UsePluginCoreLog)}", $"Notice: Updating the front-end mode requires restarting the site");
            #endregion

            return app;
        }
    }
}


