// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Plugin.PluginCore;

/// <summary>
/// 系统菜单表种子数据
/// </summary>
[IncreSeed]
public class SysMenu_PluginCore_SeedData : ISqlSugarEntitySeedData<SysMenu>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SysMenu> HasData()
    {
        return
        [
            new SysMenu{ Id=1310000000802, Pid=1310000000301, Title="应用插件", Path="/platform/pluginCore", Name="sysPluginCore", Component="/system/pluginCore/index", Icon="ele-Connection", Type=MenuTypeEnum.Menu, CreateTime=DateTime.Parse("2022-02-10 00:00:00"), OrderNo=210 },
            new SysMenu{ Id=1310000000803, Pid=1310000000802, Title="启用", Permission="sysPluginCore/enable", Type=MenuTypeEnum.Btn, CreateTime=DateTime.Parse("2022-02-10 00:00:00"), OrderNo=100 },
            new SysMenu{ Id=1310000000804, Pid=1310000000802, Title="卸载", Permission="sysPluginCore/delete", Type=MenuTypeEnum.Btn, CreateTime=DateTime.Parse("2022-02-10 00:00:00"), OrderNo=110 },
            new SysMenu{ Id=1310000000805, Pid=1310000000802, Title="禁用", Permission="sysPluginCore/disable", Type=MenuTypeEnum.Btn, CreateTime=DateTime.Parse("2022-02-10 00:00:00"), OrderNo=120 },
            new SysMenu{ Id=1310000000806, Pid=1310000000802, Title="详细", Permission="sysPluginCore/details", Type=MenuTypeEnum.Btn, CreateTime=DateTime.Parse("2022-02-10 00:00:00"), OrderNo=130 },
            new SysMenu{ Id=1310000000807, Pid=1310000000802, Title="文档", Permission="sysPluginCore/readme", Type=MenuTypeEnum.Btn, CreateTime=DateTime.Parse("2022-02-10 00:00:00"), OrderNo=140 },
            new SysMenu{ Id=1310000000808, Pid=1310000000802, Title="设置", Permission="sysPluginCore/setting", Type=MenuTypeEnum.Btn, CreateTime=DateTime.Parse("2022-02-10 00:00:00"), OrderNo=150 },
        ];
    }
}