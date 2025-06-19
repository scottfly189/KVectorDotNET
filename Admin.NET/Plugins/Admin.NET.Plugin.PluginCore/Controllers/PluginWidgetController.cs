// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Interfaces;
using PluginCore.IPlugins;

namespace PluginCore.AspNetCore.Controllers;

[Route("api/plugincore/[controller]/[action]")]
[ApiController]
[NonUnify]
public class PluginWidgetController : ControllerBase
{
    #region Fields

    private readonly IPluginFinder _pluginFinder;

    #endregion Fields

    #region Ctor

    public PluginWidgetController(IPluginFinder pluginFinder)
    {
        _pluginFinder = pluginFinder;
    }

    #endregion Ctor

    #region Actions

    #region Widget

    /// <summary>
    /// Widget
    /// </summary>
    /// <returns></returns>
    [HttpGet, HttpPost]
    public async Task<ActionResult> Widget(string widgetKey, string extraPars = "")
    {
        BaseResponseModel responseModel = new ResponseModel.BaseResponseModel();
        string responseData = "";
        widgetKey = widgetKey.Trim('"', '\'');
        string[] extraParsArr = null;
        if (!string.IsNullOrEmpty(extraPars))
        {
            extraParsArr = extraPars.Split(",", StringSplitOptions.RemoveEmptyEntries);
            extraParsArr = extraParsArr.Select(m => m.Trim('"', '\'')).ToArray();
        }
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"<!-- start:PluginCore.IPlugins.IWidgetPlugin.Widget({widgetKey},{extraPars}) -->");
        try
        {
            List<IWidgetPlugin> plugins = this._pluginFinder.EnablePlugins<IWidgetPlugin>().ToList();
            foreach (var item in plugins)
            {
                string widgetStr = await item.Widget(widgetKey, extraParsArr);
                if (!string.IsNullOrEmpty(widgetStr))
                {
                    // TODO: 配合 PluginCoreConfig.PluginWidgetDebug
                    // TODO: PluginCoreConfig 改为 Options 模式, 避免手动反复读取文件 效率低
                    //sb.AppendLine($"<!-- {item.GetType().ToString()}: -->");

                    sb.AppendLine(widgetStr);
                }
            }
        }
        catch (Exception ex)
        {
            Utils.LogUtil.Error<PluginWidgetController>(ex.ToString());
            sb.AppendLine($"<!-- Exception: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}, Details: Console -->");
        }
        sb.AppendLine($"<!-- end:PluginCore.IPlugins.IWidgetPlugin.Widget({widgetKey},{extraPars}) -->");
        responseData = sb.ToString();

        responseModel.Code = 1;
        responseModel.Message = "Load Widget Success";
        responseModel.Data = responseData;

        //return await Task.FromResult(responseModel);
        return Content(responseData, "text/html;charset=utf-8");
    }

    #endregion Widget

    #endregion Actions
}