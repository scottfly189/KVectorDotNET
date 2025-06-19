// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Models;
using System.Net;

namespace PluginCore.AspNetCore.Controllers;

/// <summary>
/// 应用中心
/// <para>插件</para>
/// </summary>
[Route("api/plugincore/admin/[controller]/[action]")]
// [PluginCoreAdminAuthorize]
[ApiController]
[NonUnify]
public class AppCenterController : ControllerBase
{
    #region Fields

    private static Dictionary<string, Task> _pluginDownloadTasks;

    #endregion Fields

    #region Ctor

    static AppCenterController()
    {
        _pluginDownloadTasks = new Dictionary<string, Task>();
    }

    public AppCenterController()
    {
    }

    #endregion Ctor

    #region Actions

    #region 插件列表

    /// <summary>
    /// 插件
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Plugins(string query = "")
    {
        BaseResponseModel responseDTO = new BaseResponseModel();
        IList<PluginRegistryResponseModel> pluginRegistryModels = new List<PluginRegistryResponseModel>();
        try
        {
            // 1. TODO: 从json文件中读取插件订阅源 registry url
            string registryUrl = "";
            // 2. TODO: 向订阅源发送 http get 获取插件列表信息  eg: http://rem-core-plugins-registry.moeci.com/?query=xxx
            IList<string> remotePluginIds = new List<string>();

            // 3. 根据本地已有 PluginId 插件情况 状态赋值
            PluginConfigModel pluginConfigModel = PluginConfigModelFactory.Create();
            // IList<string> localPluginIds = pluginConfigModel.EnabledPlugins.Concat(pluginConfigModel.DisabledPlugins).Concat(pluginConfigModel.UninstalledPlugins).ToList();
            IList<string> localPluginIds = PluginPathProvider.AllPluginFolderName();

            responseDTO.Code = 1;
            responseDTO.Message = "获取远程插件数据成功";
            responseDTO.Data = pluginRegistryModels;
        }
        catch (Exception ex)
        {
            responseDTO.Code = -1;
            responseDTO.Message = "获取远程插件数据失败: " + ex.Message;
            responseDTO.Data = pluginRegistryModels;
        }

        return await Task.FromResult(responseDTO);
    }

    #endregion 插件列表

    #region 下载插件

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> DownloadPlugin(string pluginDownloadUrl = "")
    {
        BaseResponseModel responseDTO = new BaseResponseModel();

        #region 效验

        if (string.IsNullOrEmpty(pluginDownloadUrl))
        {
            responseDTO.Code = -1;
            responseDTO.Message = "插件下载地址不正确";
            return responseDTO;
        }
        // TODO: 效验是否本地已经存在相同pluginId的插件

        #endregion 效验

        try
        {
            // 1.执行下载操作, TODO:存在问题，阻塞对性能不好，但不阻塞又不好通知用户插件下载进度，以及可能存在在插件下载过程中，用户再次点击下载
            WebClient webClient = new WebClient();
            // TODO: 插件下载文件路径
            string pluginDownloadFilePath = "";
            //webClient.DownloadFileAsync(new Uri(pluginDownloadFilePath), "");
            Task task = webClient.DownloadFileTaskAsync(pluginDownloadUrl, pluginDownloadFilePath);

            _pluginDownloadTasks.Add(pluginDownloadUrl, task);

            webClient.DownloadFileCompleted += Plugin_DownloadFileCompleted;
            webClient.DownloadProgressChanged += Plugin_DownloadProgressChanged;
            webClient.Disposed += WebClient_Disposed;

            responseDTO.Code = 1;
            responseDTO.Message = "开始下载插件";
        }
        catch (Exception ex)
        {
            responseDTO.Code = -1;
            responseDTO.Message = "下载插件失败: " + ex.Message;
        }

        return await Task.FromResult(responseDTO);
    }

    #endregion 下载插件

    #region 获取插件下载进度

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> DownloadPluginProgress()
    {
        BaseResponseModel responseDTO = new BaseResponseModel();
        try
        {
            responseDTO.Data = new { };

            responseDTO.Code = 1;
            responseDTO.Message = "获取插件下载进度成功";
        }
        catch (Exception ex)
        {
            responseDTO.Code = -1;
            responseDTO.Message = "获取插件下载进度失败: " + ex.Message;
        }

        return await Task.FromResult(responseDTO);
    }

    #endregion 获取插件下载进度

    #endregion Actions

    #region Helpers

    /// <summary>
    /// 插件下载完成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Plugin_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        Console.WriteLine("插件下载完成");
        // 1.从 _pluginDownloadTasks 中移除
        //_pluginDownloadTasks.Remove();
        // 2. 解压插件
    }

    private void Plugin_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        Console.WriteLine($"插件下载进度改变: {e.ProgressPercentage}% {e.BytesReceived}/{e.TotalBytesToReceive}");
    }

    private void WebClient_Disposed(object sender, EventArgs e)
    {
        if (sender is WebClient webClient)
        {
            Console.WriteLine(webClient.BaseAddress);
        }

        Console.WriteLine(nameof(WebClient_Disposed) + ": " + sender.ToString());
    }

    #endregion Helpers
}