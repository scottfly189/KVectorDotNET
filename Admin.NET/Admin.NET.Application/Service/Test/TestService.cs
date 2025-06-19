// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Furion.Localization;
using Furion.Logging;
using Microsoft.Extensions.Logging;

namespace Admin.NET.Application;

/// <summary>
/// 测试服务
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100, Description = "测试服务")]
public class TestService : IDynamicApiController
{
    private readonly UserManager _userManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger _logger;

    public TestService(UserManager userManager, IEventPublisher eventPublisher, ILoggerFactory loggerFactory)
    {
        _userManager = userManager;
        _eventPublisher = eventPublisher;
        _logger = loggerFactory.CreateLogger(CommonConst.SysLogCategoryName); // 日志过滤标识（会写入数据库）
    }

    [HttpGet("helloWord")]
    public Task<string> HelloWord()
    {
        return Task.FromResult($"Hello, {_userManager.Account}.");
    }

    /// <summary>
    /// Redis事件测试 - Payload 🔖
    /// </summary>
    /// <returns></returns>
    public async void EventTestAsync()
    {
        await _eventPublisher.PublishAsync(CommonConst.SendErrorMail, "Admin.NET");
    }

    /// <summary>
    /// 多语言测试
    /// </summary>
    /// <returns></returns>
    public string GetCulture()
    {
        //L.SetCulture("en-US", true);

        //var cultureName = L.GetSelectCulture().Culture.Name;

        return L.Text["Admin.NET 通用权限开发平台"];
    }

    /// <summary>
    /// 日志测试
    /// </summary>
    /// <returns></returns>
    public void LogTest()
    {
        Log.Information("Information");
        Log.Warning("Warning");
        Log.Error("Error");

        _logger.LogWarning($"信息{DateTime.Now}");
    }

    /// <summary>
    /// 匿名上传文件测试
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<SysFile> UploadFile([FromForm] UploadFileInput input)
    {
        return await App.GetRequiredService<SysFileService>().UploadFile(input);
    }
}