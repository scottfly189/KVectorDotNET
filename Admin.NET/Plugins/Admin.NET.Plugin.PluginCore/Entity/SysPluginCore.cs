﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Plugin.PluginCore;

/// <summary>
/// 系统动态插件表
/// </summary>
[SugarTable(null, "系统动态插件表")]
[SysTable]
public class SysPluginCore : EntityTenant
{
    /// <summary>
    /// 插件ID
    /// </summary>
    [SugarColumn(ColumnDescription = "插件ID", Length = 128)]
    [Required]
    public virtual string PluginId { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnDescription = "名称", Length = 64)]
    [Required, MaxLength(64)]
    public virtual string DisplayName { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    [SugarColumn(ColumnDescription = "作者", Length = 64)]
    [Required, MaxLength(64)]
    public virtual string Author { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn(ColumnDescription = "版本", Length = 64)]
    [Required, MaxLength(64)]
    public virtual string Version { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnDescription = "描述", Length = 512)]
    [MaxLength(512)]
    public string? Description { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnDescription = "排序")]
    public int OrderNo { get; set; } = 100;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnDescription = "状态")]
    public StatusEnum Status { get; set; } = StatusEnum.Enable;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnDescription = "备注", Length = 128)]
    [MaxLength(128)]
    public string? Remark { get; set; }
}