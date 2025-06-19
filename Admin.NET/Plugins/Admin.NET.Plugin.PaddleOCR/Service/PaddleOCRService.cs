// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using PaddleOCRSharp;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Plugin.PaddleOCR.Service;

/// <summary>
/// PaddleOCR 图像识别服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Description = "PaddleOCR 图像识别")]
public class PaddleOCRService : IDynamicApiController, ISingleton
{
    private readonly PaddleOCREngine _engine;
    private readonly PaddleStructureEngine _structengine;

    public PaddleOCRService()
    {
        // 自带轻量版中英文模型PP-OCRv4
        OCRModelConfig config = null;

        //// 服务器中英文模型v2
        //OCRModelConfig config = new OCRModelConfig();
        //string modelPathroot = "你的模型绝对路径文件夹";
        //config.det_infer = modelPathroot + @"\ch_ppocr_server_v2.0_det_infer";
        //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
        //config.rec_infer = modelPathroot + @"\ch_ppocr_server_v2.0_rec_infer";
        //config.keys = modelPathroot + @"\ppocr_keys.txt";

        //// 英文和数字模型v3
        //OCRModelConfig config = new OCRModelConfig();
        //string modelPathroot = "你的模型绝对路径文件夹";
        //config.det_infer = modelPathroot + @"\en_PP-OCRv3_det_infer";
        //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
        //config.rec_infer = modelPathroot + @"\en_PP-OCRv3_rec_infer";
        //config.keys = modelPathroot + @"\en_dict.txt";

        //// 中英文模型V4
        //config = new OCRModelConfig();
        //string modelPathroot = AppContext.BaseDirectory + "OcrModel\\ch_PP-OCRv4";
        //config.det_infer = modelPathroot + @"\ch_PP-OCRv4_det_infer";
        //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
        //config.rec_infer = modelPathroot + @"\ch_PP-OCRv4_rec_infer";
        //config.keys = modelPathroot + @"\ppocr_keys.txt";

        //// 服务器中英文模型V4
        //config = new OCRModelConfig();
        //string modelPathroot = "你的模型绝对路径文件夹";
        //config.det_infer = modelPathroot + @"\ch_PP-OCRv4_det_server_infer";
        //config.cls_infer = modelPathroot + @"\ch_ppocr_mobile_v2.0_cls_infer";
        //config.rec_infer = modelPathroot + @"\ch_PP-OCRv4_rec_server_infer";
        //config.keys = modelPathroot + @"\ppocr_keys.txt";

        //// 参数
        //OCRParameter oCRParameter = new OCRParameter();
        // oCRParameter.use_gpu=true; // 当使用GPU版本的预测库时，该参数打开才有效果
        // oCRParameter.enable_mkldnn = false;

        // 初始化OCR引擎
        _engine = new PaddleOCREngine(config, "");

        // 模型配置，使用默认值
        StructureModelConfig structureModelConfig = null;
        // 表格识别参数配置，使用默认值
        StructureParameter structureParameter = new();
        _structengine = new PaddleStructureEngine(structureModelConfig, structureParameter);
    }

    /// <summary>
    /// 识别身份证 🔖
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("识别身份证")]
    public async Task<dynamic> IDCardOCR([Required] IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var ocrRes = _engine.DetectText(memoryStream.ToArray());

        List<TextBlock> textBlocks = ocrRes.TextBlocks;
        var cardName = TextBlockUtil.ReadIdCardName(textBlocks);
        var cardNo = TextBlockUtil.ReadIdCardNo(textBlocks);
        var cardAddress = TextBlockUtil.ReadIdCardAddress(textBlocks);
        return await Task.FromResult(new
        {
            CardName = cardName,
            CardNo = cardNo,
            CardAddress = cardAddress
        });
    }
}