// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using PluginCore;
using PluginCore.AspNetCore.Interfaces;
using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Infrastructure;
using PluginCore.Interfaces;
using PluginCore.IPlugins;
using PluginCore.Models;
using PluginCore.Utils;

namespace Admin.NET.Plugin.PluginCore;

/// <summary>
/// 系统动态插件服务
/// </summary>
[ApiDescriptionSettings(Order = 245)]
public class SysPluginCoreService : IDynamicApiController, ITransient
{
    #region Fields

    private readonly IPluginManager _pluginManager;
    private readonly IPluginFinder _pluginFinder;
    private readonly IPluginApplicationBuilderManager _pluginApplicationBuilderManager;

    #endregion Fields

    private readonly IDynamicApiRuntimeChangeProvider _provider;
    private readonly SqlSugarRepository<SysPluginCore> _sysPluginRep;

    public SysPluginCoreService(IPluginManager pluginManager, IPluginFinder pluginFinder, IPluginApplicationBuilderManager pluginApplicationBuilderManager, IDynamicApiRuntimeChangeProvider provider,
        SqlSugarRepository<SysPluginCore> sysPluginRep)
    {
        _pluginManager = pluginManager;
        _pluginFinder = pluginFinder;
        _pluginApplicationBuilderManager = pluginApplicationBuilderManager;
        _provider = provider;
        _sysPluginRep = sysPluginRep;
    }

    /// <summary>
    /// 获取动态插件列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取动态插件列表")]
    public async Task<SqlSugarPagedList<SysPluginCore>> Page(PagePluginInput input)
    {
        return await _sysPluginRep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.DisplayName.Contains(input.Name))
            .OrderBy(u => u.OrderNo)
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 查看详细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Details"), HttpGet]
    [DisplayName("查看详细")]
    public async Task<PluginInfoResponseModel> Details(long id)
    {
        var pluginCore = await _sysPluginRep.GetByIdAsync(id);
        if (pluginCore == null) throw Oops.Oh("查询插件ID失败"); ;

        // 先移除再添加动态程序集/接口

        try
        {
            #region 效验

            var pluginId = pluginCore.PluginId;
            var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(pluginId))
            {
                throw Oops.Oh($"查看详细失败: 不存在 {pluginId} 插件");
            }

            #endregion 效验

            PluginInfoModel pluginInfoModel = PluginInfoModelFactory.Create(pluginId);
            string[] enablePluginIds = _pluginFinder.EnablePluginIds().ToArray();
            PluginInfoResponseModel pluginInfoResponseModel = PluginInfoModelToResponseModel(new List<PluginInfoModel>() { pluginInfoModel }, pluginConfigModel, enablePluginIds).FirstOrDefault();

            return pluginInfoResponseModel;
        }
        catch (Exception ex)
        {
            throw Oops.Oh("查看详细失败: " + ex.Message);
        }
    }

    #region 查看文档

    /// <summary>
    /// 查看文档
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Readme"), HttpGet]
    [DisplayName("查看文档")]
    public async Task<PluginReadmeResponseModel> Readme(long id)
    {
        var pluginCore = await _sysPluginRep.GetByIdAsync(id);
        if (pluginCore == null) throw Oops.Oh("查询插件ID失败"); ;

        try
        {
            #region 效验

            var pluginId = pluginCore.PluginId;
            var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(pluginId))
            {
                throw Oops.Oh($"查看详细失败: 不存在 {pluginId} 插件");
            }

            #endregion 效验

            PluginReadmeModel readmeModel = PluginReadmeModelFactory.Create(pluginId);
            PluginReadmeResponseModel readmeResponseModel = new PluginReadmeResponseModel();
            readmeResponseModel.Content = readmeModel?.Content ?? "";
            readmeResponseModel.PluginId = pluginId;

            return readmeResponseModel;
        }
        catch (Exception ex)
        {
            throw Oops.Oh("查看详细失败: " + ex.Message);
        }
    }

    #endregion 查看文档

    /// <summary>
    /// 卸载动态插件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Uninstall"), HttpPost]
    [DisplayName("卸载动态插件")]
    public async Task Uninstall(DeletePluginCoreInput input)
    {
        var pluginCore = await _sysPluginRep.GetByIdAsync(input.Id);
        if (pluginCore == null) throw Oops.Oh("查询插件ID失败");

        // 卸载插件 必须 先禁用插件

        #region 效验

        var pluginId = pluginCore.PluginId;
        var pluginConfigModel = PluginConfigModelFactory.Create();
        if (pluginConfigModel.EnabledPlugins.Contains(pluginId))
        {
            throw Oops.Oh("卸载失败: 请先禁用此插件");
        }
        string pluginDirStr = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
        string pluginWwwrootDirStr = Path.Combine(PluginPathProvider.PluginsWwwRootDir(), pluginId);
        if (!Directory.Exists(pluginDirStr) && !Directory.Exists(pluginWwwrootDirStr))
        {
            throw Oops.Oh("卸载失败: 此插件不存在");
        }

        #endregion 效验

        try
        {
            // PS:卸载插件必须先禁用插件，所以此时插件LoadContext已被移除释放(插件Assemblies已被释放), 此处不需移除LoadContext

            // 1.删除物理文件
            var pluginDir = new DirectoryInfo(pluginDirStr);
            if (pluginDir.Exists)
            {
                pluginDir.Delete(true);
            }
            // 虽然 已禁用 时 pluginWwwrootDirStr/pluginId 已删除, 但为确保, 还是再删除一次
            var pluginWwwrootDir = new DirectoryInfo(pluginWwwrootDirStr);
            if (pluginWwwrootDir.Exists)
            {
                pluginWwwrootDir.Delete(true);
            }

            await _sysPluginRep.DeleteAsync(u => u.Id == input.Id);
        }
        catch (Exception ex)
        {
            throw Oops.Oh("卸载失败: " + ex.Message);
        }
    }

    #region 设置

    /// <summary>
    /// 插件设置设置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Settings"), HttpGet]
    [DisplayName("插件设置设置")]
    public async Task<string> Settings(long id)
    {
        var pluginCore = await _sysPluginRep.GetByIdAsync(id);
        if (pluginCore == null) throw Oops.Oh("查询插件ID失败"); ;

        try
        {
            #region 效验

            var pluginId = pluginCore.PluginId;
            var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(pluginId))
            {
                throw Oops.Oh($"查看详细失败: 不存在 {pluginId} 插件");
            }

            #endregion 效验

            string settingsJsonStr = PluginSettingsModelFactory.Create(pluginId);

            return settingsJsonStr ?? "无设置项";
        }
        catch (Exception ex)
        {
            throw Oops.Oh("查看设置失败: " + ex.Message);
        }
    }

    /// <summary>
    /// 插件设置设置
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Settings"), HttpPost]
    [DisplayName("插件设置设置")]
    public async Task Settings(UpdatePluginCoreSettingInput input)
    {
        var pluginCore = await _sysPluginRep.GetByIdAsync(input.Id);
        if (pluginCore == null) throw Oops.Oh("查询插件ID失败"); ;

        try
        {
            #region 效验

            var pluginId = pluginCore.PluginId;
            var pluginConfigModel = PluginConfigModelFactory.Create();
            string[] localPluginIds = PluginPathProvider.AllPluginFolderName().ToArray();

            if (!localPluginIds.Contains(pluginId))
            {
                throw Oops.Oh($"查看详细失败: 不存在 {pluginId} 插件");
            }

            #endregion 效验

            input.Data = input.Data ?? "";
            PluginSettingsModelFactory.Save(pluginSettingsJsonStr: input.Data, pluginId: pluginCore.PluginId);
        }
        catch (Exception ex)
        {
            throw Oops.Oh("设置失败: " + ex.Message);
        }
    }

    #endregion 设置

    /// <summary>
    /// 启用插件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Enable"), HttpPost]
    [DisplayName("启用插件")]
    public async Task Enable(EnablePluginCoreInput input)
    {
        var pluginCore = await _sysPluginRep.GetByIdAsync(input.Id);
        if (pluginCore == null) return;

        // 移除动态程序集/接口
        var pluginConfigModel = PluginConfigModelFactory.Create();
        // 效验是否存在于 已禁用插件列表

        #region 效验

        var pluginId = pluginCore.PluginId;
        var pluginDir = new DirectoryInfo(Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId));
        if (pluginDir != null && !pluginDir.Exists)
        {
            throw Oops.Oh("启用失败: 此插件不存在");
        }
        string[] enablePluginIds = _pluginFinder.EnablePluginIds().ToArray();
        if (enablePluginIds.Contains(pluginId))
        {
            throw Oops.Oh("启用失败: 此插件已启用");
        }

        #endregion 效验

        try
        {
            // 1. 创建插件程序集加载上下文, 添加到 PluginsLoadContexts
            _pluginManager.LoadPlugin(pluginId);
            // 2. 添加到 pluginConfigModel.EnabledPlugins
            pluginConfigModel.EnabledPlugins.Add(pluginId);
            // 4.保存到 plugin.config.json
            PluginConfigModelFactory.Save(pluginConfigModel);

            // 5. 找到此插件实例
            IPlugin plugin = _pluginFinder.Plugin(pluginId);
            if (plugin == null)
            {
                // 7.启用不成功, 回滚插件状态: (1)释放插件上下文 (2)更新 plugin.config.json
                try
                {
                    _pluginManager.UnloadPlugin(pluginId);
                }
                catch (Exception ex)
                { }

                // 从 pluginConfigModel.EnabledPlugins 移除
                pluginConfigModel.EnabledPlugins.Remove(pluginId);
                // 保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);

                throw Oops.Oh("启用失败: 此插件不存在");
            }
            // 6.调取插件的 AfterEnable(), 插件开发者可在此回收资源
            var pluginEnableResult = plugin.AfterEnable();
            if (!pluginEnableResult.IsSuccess)
            {
                // 7.启用不成功, 回滚插件状态: (1)释放插件上下文 (2)更新 plugin.config.json
                try
                {
                    _pluginManager.UnloadPlugin(pluginId);
                }
                catch (Exception ex)
                { }

                // 从 pluginConfigModel.EnabledPlugins 移除
                pluginConfigModel.EnabledPlugins.Remove(pluginId);
                // 保存到 plugin.config.json
                PluginConfigModelFactory.Save(pluginConfigModel);
                throw Oops.Oh("启用失败: 来自插件的错误信息: " + pluginEnableResult.Message);
            }

            // 7. ReBuild
            this._pluginApplicationBuilderManager.ReBuild();

            // 8. 尝试复制 插件下的 wwwroot 到 Plugins_wwwroot
            string wwwRootDir = PluginPathProvider.WwwRootDir(pluginId);
            if (Directory.Exists(wwwRootDir))
            {
                string targetDir = PluginPathProvider.PluginWwwRootDir(pluginId);
                FileUtil.CopyFolder(wwwRootDir, targetDir);
            }

            //9.载入Furion动态插件
            var pluginMainAssembly = _pluginManager.GetPluginAssembly(pluginId);
            // 将程序集添加进动态 WebAPI 应用部件
            _provider.AddAssembliesWithNotifyChanges(pluginMainAssembly);

            await _sysPluginRep.UpdateAsync(u => new SysPluginCore() { Status = StatusEnum.Enable }, u => u.Id == input.Id);
        }
        catch (Exception ex)
        {
            throw Oops.Oh("启用失败:  " + ex.Message);
        }
    }

    /// <summary>
    /// 禁用插件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Disable"), HttpPost]
    [DisplayName("禁用插件")]
    public async Task Disable(EnablePluginCoreInput input)
    {
        var pluginCore = await _sysPluginRep.GetByIdAsync(input.Id);
        if (pluginCore == null) return;

        // 移除动态程序集/

        #region 效验

        var pluginId = pluginCore.PluginId;
        var pluginConfigModel = PluginConfigModelFactory.Create();
        // string[] enablePluginIds = _pluginFinder.EnablePluginIds().ToArray();
        // // 效验是否存在于 已启用插件列表
        // if (!enablePluginIds.Contains(pluginId))
        // {
        //     responseData.Code = -1;
        //     responseData.Message = "禁用失败: 此插件不存在, 或未启用";
        //     return await Task.FromResult(responseData);
        // }

        #endregion 效验

        try
        {
            // 1. 找到此插件实例
            IPlugin plugin = _pluginFinder.Plugin(pluginId);
            if (plugin == null)
            {
                throw Oops.Oh("禁用失败: 此插件不存在, 或未启用");
            }
            try
            {
                // 2.调取插件的 BeforeDisable(), 插件开发者可在此回收资源
                var pluginDisableResult = plugin.BeforeDisable();
                if (!pluginDisableResult.IsSuccess)
                {
                    throw Oops.Oh("禁用失败: 来自插件的错误信息: " + pluginDisableResult.Message);
                }
                // 3.移除插件对应的程序集加载上下文
                _pluginManager.UnloadPlugin(pluginId);
                // 3.1. ReBuild
                this._pluginApplicationBuilderManager.ReBuild();
                if (pluginConfigModel.EnabledPlugins.Contains(pluginId))
                {
                    // 4.从 pluginConfigModel.EnabledPlugins 移除
                    pluginConfigModel.EnabledPlugins.Remove(pluginId);
                    // 5.保存到 plugin.config.json
                    PluginConfigModelFactory.Save(pluginConfigModel);
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error<SysPluginCoreService>(ex.ToString());
                throw Oops.Oh("禁用失败: 此插件不存在, 或未启用");
            }

            // 7. 尝试移除 Plugins_wwwroot/PluginId
            string pluginWwwRootDir = PluginPathProvider.PluginWwwRootDir(pluginId);
            if (Directory.Exists(pluginWwwRootDir))
            {
                Directory.Delete(pluginWwwRootDir, true);
            }
            //8.移除Furion动态插件

            //9.载入Furion动态插件
            var pluginMainAssembly = _pluginManager.GetPluginAssembly(pluginId);
            // 将程序集添加进动态 WebAPI 应用部件
            _provider.RemoveAssembliesWithNotifyChanges(pluginMainAssembly);

            await _sysPluginRep.UpdateAsync(u => new SysPluginCore() { Status = StatusEnum.Disable }, u => u.Id == input.Id);
        }
        catch (Exception ex)
        {
            throw Oops.Oh("禁用失败: " + ex.Message);
        }
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    [DisplayName("上传文件")]
    public async Task UploadFile([Required] IFormFile file, [FromQuery] string? path)
    {
        var sysFile = await HandleUploadFile(file, path);
        //return new FileOutput
        //{
        //};
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="savePath">路径</param>
    /// <returns></returns>
    [NonAction]
    private async Task<BaseResponseModel> HandleUploadFile(IFormFile file, string savePath)
    {
        if (file == null) throw Oops.Oh(ErrorCodeEnum.D8000);

        var path = savePath;

        BaseResponseModel responseData = new BaseResponseModel();

        #region 效验

        if (file == null)
        {
            throw Oops.Oh("上传的文件不能为空");
            //responseData.Code = -1;
            //responseData.Message = "上传的文件不能为空";
            //return responseData;
        }
        //文件后缀
        string fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名
                                                                // 类型标记
        UploadFileType uploadFileType = UploadFileType.NoAllowedType;
        switch (fileExtension)
        {
            case ".zip":
                uploadFileType = UploadFileType.Zip;
                break;

            case ".nupkg":
                uploadFileType = UploadFileType.Nupkg;
                break;
        }

        if (fileExtension != ".zip" && fileExtension != ".nupkg")
        {
            throw Oops.Oh("只能上传 zip 或 nupkg 格式文件");
            //responseData.Code = -1;
            //// nupkg 其实就是 zip
            //responseData.Message = "只能上传 zip 或 nupkg 格式文件";
            //return responseData;
        }
        // PluginCore.AspNetCore-v1.0.2 起 不再限制插件上传大小
        //判断文件大小
        //var fileSize = file.Length;
        //if (fileSize > 1024 * 1024 * 5) // 5M
        //{
        //    responseData.Code = -1;
        //    responseData.Message = "上传的文件不能大于5MB";
        //    return responseData;
        //}

        #endregion 效验

        try
        {
            // 1.先上传到 临时插件上传目录, 用Guid.zip作为保存文件名
            string tempZipFilePath = Path.Combine(PluginPathProvider.TempPluginUploadDir(), Guid.NewGuid() + ".zip");
            using (var fs = System.IO.File.Create(tempZipFilePath))
            {
                file.CopyTo(fs); //将上传的文件文件流，复制到fs中
                fs.Flush();//清空文件流
            }
            // 2.解压
            bool isDecomparessSuccess = false;
            if (uploadFileType == UploadFileType.Zip)
            {
                isDecomparessSuccess = ZipHelper.DecomparessFile(tempZipFilePath, tempZipFilePath.Replace(".zip", ""));
            }
            else if (uploadFileType == UploadFileType.Nupkg)
            {
                isDecomparessSuccess = NupkgService.DecomparessFile(tempZipFilePath, tempZipFilePath.Replace(".zip", ""));
            }

            // 3.删除原压缩包
            System.IO.File.Delete(tempZipFilePath);
            if (!isDecomparessSuccess)
            {
                throw Oops.Oh("解压插件压缩包失败");

                //responseData.Code = -1;
                //responseData.Message = "解压插件压缩包失败";
                //return responseData;
            }
            // 4.读取其中的info.json, 获取 PluginId 值
            PluginInfoModel pluginInfoModel = PluginInfoModelFactory.ReadPluginDir(tempZipFilePath.Replace(".zip", ""));
            if (pluginInfoModel == null || string.IsNullOrEmpty(pluginInfoModel.PluginId))
            {
                // 记得删除已不再需要的临时插件文件夹
                Directory.Delete(tempZipFilePath.Replace(".zip", ""), true);
                throw Oops.Oh("不合法的插件");

                //responseData.Code = -1;
                //responseData.Message = "不合法的插件";
                //return responseData;
            }
            string pluginId = pluginInfoModel.PluginId;
            // 5.检索 此 PluginId 是否本地插件已存在
            var pluginConfigModel = PluginConfigModelFactory.Create();
            // 本地已经存在的 PluginId
            IList<string> localExistPluginIds = PluginPathProvider.AllPluginFolderName();
            if (localExistPluginIds.Contains(pluginId))
            {
                // 记得删除已不再需要的临时插件文件夹
                Directory.Delete(tempZipFilePath.Replace(".zip", ""), true);
                throw Oops.Oh($"本地已有此插件 (PluginId: {pluginId}), 请前往插件列表删除后, 再上传");

                //responseData.Code = -1;
                //responseData.Message = $"本地已有此插件 (PluginId: {pluginId}), 请前往插件列表删除后, 再上传";
                //return responseData;
            }
            // 6.本地无此插件 -> 移动插件文件夹到 Plugins 下, 并以 PluginId 为插件文件夹名
            string pluginsRootPath = PluginPathProvider.PluginsRootPath();
            string newPluginDir = Path.Combine(pluginsRootPath, pluginId);
            Directory.Move(tempZipFilePath.Replace(".zip", ""), newPluginDir);

            // 7. 放入 Plugins 中, 默认为 已禁用

            var pluginCore = new SysPluginCore();
            pluginCore.PluginId = pluginId;
            pluginCore.DisplayName = pluginInfoModel.DisplayName;
            pluginCore.Description = pluginInfoModel.Description;
            pluginCore.Author = pluginInfoModel.Author;
            pluginCore.Version = pluginInfoModel.Version;
            pluginCore.Status = StatusEnum.Disable;
            await _sysPluginRep.InsertAsync(pluginCore.Adapt<SysPluginCore>());

            responseData.Code = 1;
            responseData.Message = $"上传插件成功 (PluginId: {pluginId})";
        }
        catch (Exception ex)
        {
            throw Oops.Oh("上传插件失败: " + ex.Message);

            //responseData.Code = -1;
            //responseData.Message = "上传插件失败: " + ex.Message;
            //ex = ex.InnerException;
            //while (ex != null)
            //{
            //    responseData.Message += " - " + ex.InnerException.Message;
            //    ex = ex.InnerException;
            //}
        }

        return responseData;
    }

    #region Helpers

    [NonAction]
    private IList<PluginInfoResponseModel> PluginInfoModelToResponseModel(IList<PluginInfoModel> pluginInfoModels, PluginConfigModel pluginConfigModel, string[] enablePluginIds)
    {
        // 获取 Plugins 下所有插件
        // DirectoryInfo pluginsDir = new DirectoryInfo(PluginPathProvider.PluginsRootPath());
        // List<string> pluginIds = pluginsDir?.GetDirectories()?.Select(m => m.Name)?.ToList() ?? new List<string>();

        IList<PluginInfoResponseModel> responseModels = new List<PluginInfoResponseModel>();

        #region 添加插件状态信息

        foreach (var model in pluginInfoModels)
        {
            PluginInfoResponseModel responseModel = new PluginInfoResponseModel();
            responseModel.Author = model.Author;
            responseModel.Description = model.Description;
            responseModel.DisplayName = model.DisplayName;
            responseModel.PluginId = model.PluginId;
            responseModel.SupportedVersions = model.SupportedVersions;
            responseModel.Version = model.Version;
            responseModel.DependPlugins = model.DependPlugins;

            if (pluginConfigModel.EnabledPlugins.Contains(model.PluginId) && !enablePluginIds.Contains(model.PluginId))
            {
                // 错误情况: 配置 标识 已启用, 但实际没有启用成功
                pluginConfigModel.EnabledPlugins.Remove(model.PluginId);
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseModel.Status = PluginStatus.Disabled;
            }
            else if (!pluginConfigModel.EnabledPlugins.Contains(model.PluginId) && enablePluginIds.Contains(model.PluginId))
            {
                // 错误情况: 配置没有标识 已启用, 但实际 已启用
                pluginConfigModel.EnabledPlugins.Add(model.PluginId);
                PluginConfigModelFactory.Save(pluginConfigModel);

                responseModel.Status = PluginStatus.Enabled;
            }
            else if (pluginConfigModel.EnabledPlugins.Contains(model.PluginId) && enablePluginIds.Contains(model.PluginId))
            {
                responseModel.Status = PluginStatus.Enabled;
            }
            else
            {
                responseModel.Status = PluginStatus.Disabled;
            }
            responseModels.Add(responseModel);
        }

        #endregion 添加插件状态信息

        return responseModels;
    }

    public enum UploadFileType
    {
        NoAllowedType = 0,
        Zip = 1,
        Nupkg = 2
    }

    #endregion Helpers
}