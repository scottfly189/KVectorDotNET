// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using PluginCore.AspNetCore.Extensions;
using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Interfaces;
using System.Runtime.Loader;

namespace PluginCore.AspNetCore.Controllers;

/// <summary>
/// [ASP.NET Core — 依赖注入\_啊晚的博客-CSDN博客\_asp.net core 依赖注入](https://blog.csdn.net/weixin_37648525/article/details/127942292)
/// [ASP.NET Core中的依赖注入（3）: 服务的注册与提供 - Artech - 博客园](https://www.cnblogs.com/artech/p/asp-net-core-di-register.html)
/// [ASP.NET Core中的依赖注入（5）: ServiceProvider实现揭秘 【总体设计 】 - Artech - 博客园](https://www.cnblogs.com/artech/p/asp-net-core-di-service-provider-1.html)
/// [dotnet/ServiceProvider.cs at main · dotnet/dotnet](https://github.com/dotnet/dotnet/blob/main/src/runtime/src/libraries/Microsoft.Extensions.DependencyInjection/src/ServiceProvider.cs)
/// [Net6 DI源码分析Part2 Engine,ServiceProvider - 一身大膘 - 博客园](https://www.cnblogs.com/hts92/p/15800990.html)
/// [【特别的骚气】asp.net core运行时注入服务，实现类库热插拔 - 四处观察 - 博客园](https://www.cnblogs.com/1996-Chinese-Chen/p/16154218.html)
///
/// ActivatorUtilities.CreateInstance<PluginCore.IPlugins.IPlugin>(serviceProvider, "test");
/// ActivatorUtilities.GetServiceOrCreateInstance<PluginCore.IPlugins.IPlugin>(serviceProvider);
/// </summary>
[Route("api/plugincore/admin/[controller]/[action]")]
//[PluginCoreAdminAuthorize]
[ApiController]
[NonUnify]
public class DebugController : ControllerBase
{
    #region Fields

    private readonly IPluginContextManager _pluginContextManager;

    #endregion Fields

    #region Ctor

    public DebugController(IPluginContextManager pluginContextManager)
    {
        _pluginContextManager = pluginContextManager;
    }

    #endregion Ctor

    #region Actions

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> PluginContexts()
    {
        BaseResponseModel responseModel = new BaseResponseModel();
        try
        {
            var pluginContextList = _pluginContextManager.All();
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
            foreach (var pluginContext in pluginContextList)
            {
                keyValuePairs.Add($"{pluginContext.GetType().ToString()} - {pluginContext.PluginId} - {pluginContext.GetHashCode()}", pluginContext.Assemblies.Select(m => m.FullName).ToList());
            }

            responseModel.Code = 1;
            responseModel.Message = "success";
            responseModel.Data = keyValuePairs;
        }
        catch (Exception ex)
        {
            responseModel.Code = -1;
            responseModel.Message = "error";
            responseModel.Data = ex.ToString();
        }

        return await Task.FromResult(responseModel);
    }

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> AssemblyLoadContexts()
    {
        BaseResponseModel responseModel = new BaseResponseModel();
        try
        {
            var assemblyLoadContextDefault = AssemblyLoadContext.Default;
            var assemblyLoadContextAll = AssemblyLoadContext.All;
            var responseDataModel = new AssemblyLoadContextsResponseDataModel();
            responseDataModel.Default = new AssemblyLoadContextsResponseDataModel.AssemblyLoadContextModel
            {
                Name = assemblyLoadContextDefault.Name,
                Type = assemblyLoadContextDefault.GetType().ToString(),
                Assemblies = assemblyLoadContextDefault.Assemblies.Select(m => new AssemblyModel { FullName = m.FullName, DefinedTypes = m.DefinedTypes.Select(m => m.FullName).ToList() }).ToList()
            };
            responseDataModel.All = assemblyLoadContextAll.Select(item => new AssemblyLoadContextsResponseDataModel.AssemblyLoadContextModel
            {
                Name = item.Name,
                Type = item.GetType().ToString(),
                Assemblies = item.Assemblies.Select(m => new AssemblyModel { FullName = m.FullName, DefinedTypes = m.DefinedTypes.Select(m => m.FullName).ToList() }).ToList()
            }).ToList();

            responseModel.Code = 1;
            responseModel.Message = "success";
            responseModel.Data = responseDataModel;
        }
        catch (Exception ex)
        {
            responseModel.Code = -1;
            responseModel.Message = "error";
            responseModel.Data = ex.ToString();
        }

        return await Task.FromResult(responseModel);
    }

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Assemblies()
    {
        BaseResponseModel responseModel = new BaseResponseModel();
        try
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<AssemblyModel> assemblyModels = new List<AssemblyModel>();
            foreach (var item in assemblies)
            {
                assemblyModels.Add(new AssemblyModel
                {
                    FullName = item.FullName,
                    DefinedTypes = item.DefinedTypes.Select(m => m.FullName).ToList()
                });
            }

            responseModel.Code = 1;
            responseModel.Message = "success";
            responseModel.Data = assemblyModels;
        }
        catch (Exception ex)
        {
            responseModel.Code = -1;
            responseModel.Message = "error";
            responseModel.Data = ex.ToString();
        }

        return await Task.FromResult(responseModel);
    }

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Services([FromServices] IServiceProvider serviceProvider)
    {
        BaseResponseModel responseModel = new BaseResponseModel();
        try
        {
            //IServiceProvider serviceProvider = HttpContext.RequestServices;
            //var provider = serviceProvider.GetType().GetProperty("RootProvider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //var serviceField = provider.GetType().GetField("_realizedServices", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //var serviceValue = serviceField.GetValue(provider);
            //var funcType = serviceField.FieldType.GetGenericArguments()[1].GetGenericArguments()[0];
            //ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object?>> realizedServices = (ConcurrentDictionary<Type, Func<ServiceProviderEngineScope, object?>>)serviceValue;

            // 获取所有已经注册的服务
            var allService = serviceProvider.GetAllServiceDescriptors();

            List<ServiceModel> serviceModels = new List<ServiceModel>();
            foreach (var item in allService)
            {
                serviceModels.Add(new ServiceModel
                {
                    Type = item.Key.ToString(),
                    ImplementationType = item.Value.ImplementationType?.ToString() ?? "",
                    Lifetime = item.Value.Lifetime.ToString(),
                    TypeAssembly = new AssemblyModel
                    {
                        FullName = item.Key.Assembly.FullName,
                    },
                    ImplementationTypeAssembly = new AssemblyModel
                    {
                        FullName = item.Value.ImplementationType?.Assembly?.FullName ?? ""
                    }
                });
            }

            responseModel.Code = 1;
            responseModel.Message = "success";
            responseModel.Data = serviceModels;
        }
        catch (Exception ex)
        {
            responseModel.Code = -1;
            responseModel.Message = "error";
            responseModel.Data = ex.ToString();
        }

        return await Task.FromResult(responseModel);
    }

    #endregion Actions

    public sealed class AssemblyLoadContextsResponseDataModel
    {
        public AssemblyLoadContextModel Default
        {
            get; set;
        }

        public List<AssemblyLoadContextModel> All
        {
            get; set;
        }

        public sealed class AssemblyLoadContextModel
        {
            public string Name
            {
                get; set;
            }

            public string Type
            {
                get; set;
            }

            public List<AssemblyModel> Assemblies
            {
                get; set;
            }
        }
    }

    public sealed class AssembliesResponseDataModel
    {
    }

    public sealed class ServiceModel
    {
        public string Type
        {
            get; set;
        }

        public string ImplementationType
        {
            get; set;
        }

        public string Lifetime
        {
            get; set;
        }

        public AssemblyModel TypeAssembly
        {
            get; set;
        }

        public AssemblyModel ImplementationTypeAssembly
        {
            get; set;
        }
    }

    public sealed class AssemblyModel
    {
        public string FullName
        {
            get; set;
        }

        public List<string> DefinedTypes
        {
            get; set;
        }
    }
}