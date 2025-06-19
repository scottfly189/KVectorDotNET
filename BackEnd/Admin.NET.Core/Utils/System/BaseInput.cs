﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core;

/// <summary>
/// 主键Id输入参数
/// </summary>
public class BaseIdInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "Id不能为空")]
    [DataValidation(ValidationTypes.Numeric)]
    public virtual long Id { get; set; }
}

/// <summary>
/// Code
/// </summary>
public class CodeInput
{
    /// <summary>
    /// Code
    /// </summary>
    ///<example></example>
    [Required(ErrorMessage = "Code不能为空"), MinLength(10, ErrorMessage = "Code错误")]
    public string Code { get; set; }
}

/// <summary>
/// OpenId
/// </summary>
public class OpenIdInputDto //: WechatUserLogin
{
    /// <summary>
    /// OpenId
    /// </summary>
    ///<example></example>
    [Required(ErrorMessage = "微信标识不能为空"), MinLength(10, ErrorMessage = "微信标识长错误")]
    public string OpenId { get; set; }
}

/// <summary>
/// Url
/// </summary>
public class UrlInput //: SignatureInput
{
    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; set; }
}

/// <summary>
/// Phone
/// </summary>
public class PhoneInput
{
    /// <summary>
    /// 手机号码
    /// </summary>
    /// <example>13980134216</example>
    [Required(ErrorMessage = "手机号码不能为空")]
    [DataValidation(ValidationTypes.PhoneNumber, ErrorMessage = "手机号码不正确")]
    public string Phone { get; set; }
}