// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using PluginCore.AspNetCore.RequestModel.User;
using PluginCore.AspNetCore.ResponseModel;
using PluginCore.Config;


namespace PluginCore.AspNetCore.Controllers;

[Route("api/plugincore/admin/[controller]/[action]")]
[ApiController]
[NonUnify]
public class UserController : ControllerBase
{
    public string RemoteFronted
    {
        get
        {
            return PluginCore.Config.PluginCoreConfigFactory.Create().RemoteFrontend;
        }
    }

    private readonly UserManager _userManager;
    private readonly SqlSugarRepository<SysUser> _sysUserRep;
    private readonly SysOrgService _sysOrgService;
    private readonly SysUserExtOrgService _sysUserExtOrgService;
    private readonly SysUserRoleService _sysUserRoleService;
    private readonly SysConfigService _sysConfigService;

    public UserController(UserManager userManager,
        SqlSugarRepository<SysUser> sysUserRep,
        SysOrgService sysOrgService,
        SysUserExtOrgService sysUserExtOrgService,
        SysUserRoleService sysUserRoleService,
        SysConfigService sysConfigService)
    {
        _userManager = userManager;
        _sysUserRep = sysUserRep;
        _sysOrgService = sysOrgService;
        _sysUserExtOrgService = sysUserExtOrgService;
        _sysUserRoleService = sysUserRoleService;
        _sysConfigService = sysConfigService;
    }

    /// <summary>
    /// 登录系统
    /// </summary>
    /// <param name="input"></param>
    /// <remarks>用户名/密码：superadmin/123456</remarks>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet, HttpPost]
    [DisplayName("登录系统")]
    public async Task<ActionResult<BaseResponseModel>> Login([FromBody] LoginRequestModel input)
    {
        BaseResponseModel responseModel = new BaseResponseModel();

        // 账号是否存在
        var user = await _sysUserRep.AsQueryable().Includes(t => t.SysOrg).Filter(null, true).FirstAsync(u => u.Account.Equals(input.UserName));
        _ = user ?? throw Oops.Oh(ErrorCodeEnum.D0009);

        // 账号是否被冻结
        if (user.Status == StatusEnum.Disable)
            throw Oops.Oh(ErrorCodeEnum.D1017);

        // 租户是否被禁用
        var tenant = await _sysUserRep.ChangeRepository<SqlSugarRepository<SysTenant>>().GetFirstAsync(u => u.Id == user.TenantId);
        if (tenant != null && tenant.Status == StatusEnum.Disable)
            throw Oops.Oh(ErrorCodeEnum.Z1003);

        // 密码是否正确
        if (CryptogramUtil.CryptoType == CryptogramEnum.MD5.ToString())
        {
            if (user.Password != MD5Encryption.Encrypt(input.Password))
                throw Oops.Oh(ErrorCodeEnum.D1000);
        }
        else
        {
            if (CryptogramUtil.Decrypt(user.Password) != input.Password)
                throw Oops.Oh(ErrorCodeEnum.D1000);
        }

        var tokenExpire = await _sysConfigService.GetTokenExpire();
        var refreshTokenExpire = await _sysConfigService.GetRefreshTokenExpire();

        // 生成Token令牌
        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
    {
        { ClaimConst.UserId, user.Id },
        { ClaimConst.TenantId, user.TenantId },
        { ClaimConst.Account, user.Account },
        { ClaimConst.RealName, user.RealName },
        { ClaimConst.AccountType, user.AccountType },
        { ClaimConst.OrgId, user.OrgId },
        { ClaimConst.OrgName, user.SysOrg?.Name },
        //{ ClaimConst.OrgType, user.SysOrg?.OrgType },
    }, tokenExpire);

        // 生成刷新Token令牌
        var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, refreshTokenExpire);

        responseModel.Code = 1;
        responseModel.Message = "登录成功";
        responseModel.Data = new
        {
            token = accessToken,
            userName = user.NickName,
            RefreshToken = refreshToken
        };
        return await Task.FromResult(responseModel);
    }

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Logout()
    {
        BaseResponseModel responseModel = new BaseResponseModel()
        {
            Code = 1,
            Message = "退出登录成功"
        };

        return await Task.FromResult(responseModel);
    }

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Info()
    {
        BaseResponseModel responseModel = new BaseResponseModel();

        try
        {
            string adminUserName = PluginCoreConfigFactory.Create().Admin.UserName;

            responseModel.Code = 1;
            responseModel.Message = "成功";
            responseModel.Data = new
            {
                name = adminUserName,
                //avatar = this.RemoteFronted + "/images/avatar.gif"
                avatar = ""
            };
        }
        catch (Exception ex)
        {
            responseModel.Code = -1;
            responseModel.Message = "失败: " + ex.Message;
        }

        return await Task.FromResult(responseModel);
    }

    [HttpGet, HttpPost]
    public async Task<ActionResult<BaseResponseModel>> Update([FromBody] UpdateRequestModel requestModel)
    {
        BaseResponseModel responseModel = new BaseResponseModel();

        try
        {
            PluginCoreConfig pluginCoreConfig = PluginCoreConfigFactory.Create();
            pluginCoreConfig.Admin.UserName = requestModel.UserName;
            pluginCoreConfig.Admin.Password = requestModel.Password;
            PluginCoreConfigFactory.Save(pluginCoreConfig);

            responseModel.Code = 1;
            responseModel.Message = "修改成功, 需要重新登录";
        }
        catch (Exception ex)
        {
            responseModel.Code = -1;
            responseModel.Message = "失败: " + ex.Message;
        }

        return await Task.FromResult(responseModel);
    }
}