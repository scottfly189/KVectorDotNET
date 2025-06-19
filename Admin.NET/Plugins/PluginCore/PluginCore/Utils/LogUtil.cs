// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace PluginCore.Utils
{
    public class LogUtil
    {
        public const string LogCategoryName = nameof(PluginCore);

        private static IServiceScopeFactory _serviceScopeFactory;

        public static void Initialize(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public static void Info<T>(string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogInformation($"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Info(string categoryName, string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // null
                // ILogger? service = scope.ServiceProvider.GetService<ILogger>();
                ILogger? service = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(categoryName: categoryName) ?? null;
                if (service != null)
                {
                    service.LogInformation($"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Warn<T>(string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogWarning($"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Warn(string categoryName, string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // null
                // ILogger? service = scope.ServiceProvider.GetService<ILogger>();
                ILogger? service = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(categoryName: categoryName) ?? null;
                if (service != null)
                {
                    service.LogWarning($"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Warn<T>(Exception ex, string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogWarning(ex, $"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Warn(string categoryName, Exception ex, string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // null
                // ILogger? service = scope.ServiceProvider.GetService<ILogger>();
                ILogger? service = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(categoryName: categoryName) ?? null;
                if (service != null)
                {
                    service.LogWarning(ex, $"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Error<T>(string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogError($"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Error(string categoryName, string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // null
                // ILogger? service = scope.ServiceProvider.GetService<ILogger>();
                ILogger? service = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(categoryName: categoryName) ?? null;
                if (service != null)
                {
                    service.LogError($"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Error<T>(Exception ex, string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogError(ex, $"{LogCategoryName}: {message}");
                }
            }
        }

        public static void Error(string categoryName, Exception ex, string message)
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                // null
                // ILogger? service = scope.ServiceProvider.GetService<ILogger>();
                ILogger? service = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger(categoryName: categoryName) ?? null;
                if (service != null)
                {
                    service.LogError(ex, $"{LogCategoryName}: {message}");
                }
            }
        }

        public static void PluginBehavior<T>(T plugin, Type iplugin, string methodName)
            where T : IPlugins.IPlugin
        {
            if (_serviceScopeFactory == null)
            {
                return;
            }
            // TODO: Bug: 无法区别 相同方法名, 参数不同的 重载方法
            MethodInfo behavior = iplugin.GetMethods().FirstOrDefault(m => m.Name == methodName);

            ParameterInfo[] pars = behavior.GetParameters();
            string parsStr = string.Empty;
            if (pars != null && pars.Length > 0)
            {
                var parTypes = pars.OrderBy(m => m.Position).Select(m => m.ParameterType.Name).ToArray();
                parsStr = string.Join(", ", parTypes);
            }

            // A程序集.APlugin
            string pluginStr = plugin.GetType().ToString();
            // A程序集.AClass.AMethod()
            string message = $"{behavior.DeclaringType.ToString()}.{behavior.Name}({parsStr})";

            // 2. 日志输出
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ILogger<T>? service = scope.ServiceProvider.GetService<ILogger<T>>();
                if (service != null)
                {
                    service.LogInformation($"{LogCategoryName}: {message}");
                }
            }
        }

    }
}


