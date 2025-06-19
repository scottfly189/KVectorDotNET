﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core.Service;

public class WechatPayOutput
{
    /// <summary>
    /// OpenId
    /// </summary>
    public string OpenId { get; set; }

    /// <summary>
    /// 商户(支付交易)单号,微信唯一
    /// </summary>
    public string OutTradeNumber { get; set; }

    /// <summary>
    /// 关联的商户(商品交易)订单号
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 关联的商户订单状态(或者为第几次支付，有些订单涉及多次支付，比如先付预付款，后补尾款)
    /// </summary>
    public int OrderStatus { get; set; }

    /// <summary>
    /// 订单金额
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public string Attachment { get; set; }

    /// <summary>
    /// 优惠标记
    /// </summary>
    public string GoodsTag { get; set; }

    /// <summary>
    /// 支付发起时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 支付完成时间
    /// </summary>
    public DateTime SuccessTime { get; set; }

    /// <summary>
    /// 交易状态(支付成功后，微信返回)
    /// </summary>
    public string? TradeState { get; set; }

    /// <summary>
    /// 交易状态描述(支付成功后，微信返回)
    /// </summary>
    public string? TradeStateDescription { get; set; }
}

public class CreatePayTransactionOutput
{
    public string PrepayId { get; set; }
    public string OutTradeNumber { get; set; }
    public WechatPayParaOutput SingInfo { get; set; }
}

public class WechatPayParaOutput
{
    public string AppId { get; set; }

    public string TimeStamp { get; set; }

    public string NonceStr { get; set; }

    public string Package { get; set; }

    public string SignType { get; set; }

    public string PaySign { get; set; }
}

public class CreatePayTransactionNativeOutput
{
    public string OutTradeNumber { get; set; }

    public string QrcodeUrl { get; set; }
}