// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.Interfaces;
using System.ComponentModel;

namespace Admin.NET.Plugin.Pay.Alipay.Service;
 
/// <summary>
/// 系统动态插件服务
/// </summary>
[ApiDescriptionSettings("自定义插件", Name = "Alipay", Order = 100, Description = "支付宝支付插件")] 
[AllowAnonymous]
public class AlipayPluginCoreService : IDynamicApiController, IScoped
{
    #region Fields
    private readonly IPluginManager _pluginManager;
    private readonly IPluginFinder _pluginFinder;
    private readonly IPluginApplicationBuilderManager _pluginApplicationBuilderManager;
    #endregion
    private readonly IDynamicApiRuntimeChangeProvider _provider;


    public AlipayPluginCoreService(IPluginManager pluginManager, IPluginFinder pluginFinder, IPluginApplicationBuilderManager pluginApplicationBuilderManager, IDynamicApiRuntimeChangeProvider provider)
    {
        _pluginManager = pluginManager;
        _pluginFinder = pluginFinder;
        _pluginApplicationBuilderManager = pluginApplicationBuilderManager;
        _provider = provider;
    }

    /// <summary>
    /// 获取动态插件列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取动态插件列表")]
    [HttpGet]
    [ApiDescriptionSettings(Name = "Page")]
    public Task<string> Page()
    {
        return Task.FromResult("測試插件");
    }


}