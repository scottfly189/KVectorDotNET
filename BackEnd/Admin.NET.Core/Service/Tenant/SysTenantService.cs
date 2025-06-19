// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core.Service;

/// <summary>
/// 系统租户管理服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 390, Description = "租户管理")]
public class SysTenantService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<SysTenant> _sysTenantRep;
    private readonly SqlSugarRepository<SysOrg> _sysOrgRep;
    private readonly SqlSugarRepository<SysRole> _sysRoleRep;
    private readonly SqlSugarRepository<SysPos> _sysPosRep;
    private readonly SqlSugarRepository<SysUser> _sysUserRep;
    private readonly SqlSugarRepository<SysMenu> _sysMenuRep;
    private readonly SqlSugarRepository<SysUserExtOrg> _sysUserExtOrgRep;
    private readonly SqlSugarRepository<SysRoleMenu> _sysRoleMenuRep;
    private readonly SqlSugarRepository<SysUserRole> _userRoleRep;
    private readonly SqlSugarRepository<SysFile> _fileRep;
    private readonly SysUserRoleService _sysUserRoleService;
    private readonly SysRoleService _sysRoleService;
    private readonly SysRoleMenuService _sysRoleMenuService;
    private readonly SysConfigService _sysConfigService;
    private readonly SysCacheService _sysCacheService;
    private readonly SysFileService _sysFileService;
    private readonly IEventPublisher _eventPublisher;

    public SysTenantService(SqlSugarRepository<SysTenant> sysTenantRep,
        SqlSugarRepository<SysOrg> sysOrgRep,
        SqlSugarRepository<SysRole> sysRoleRep,
        SqlSugarRepository<SysPos> sysPosRep,
        SqlSugarRepository<SysUser> sysUserRep,
        SqlSugarRepository<SysMenu> sysMenuRep,
        SqlSugarRepository<SysUserExtOrg> sysUserExtOrgRep,
        SqlSugarRepository<SysRoleMenu> sysRoleMenuRep,
        SqlSugarRepository<SysUserRole> userRoleRep,
        SqlSugarRepository<SysFile> fileRep,
        SysUserRoleService sysUserRoleService,
        SysRoleService sysRoleService,
        SysRoleMenuService sysRoleMenuService,
        SysConfigService sysConfigService,
        SysCacheService sysCacheService,
        SysFileService sysFileService,
        IEventPublisher eventPublisher)
    {
        _sysTenantRep = sysTenantRep;
        _sysOrgRep = sysOrgRep;
        _sysRoleRep = sysRoleRep;
        _sysPosRep = sysPosRep;
        _sysUserRep = sysUserRep;
        _sysMenuRep = sysMenuRep;
        _sysUserExtOrgRep = sysUserExtOrgRep;
        _sysRoleMenuRep = sysRoleMenuRep;
        _userRoleRep = userRoleRep;
        _fileRep = fileRep;
        _sysUserRoleService = sysUserRoleService;
        _sysRoleService = sysRoleService;
        _sysRoleMenuService = sysRoleMenuService;
        _sysConfigService = sysConfigService;
        _sysCacheService = sysCacheService;
        _sysFileService = sysFileService;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// 获取租户分页列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取租户分页列表")]
    public async Task<SqlSugarPagedList<TenantOutput>> Page(PageTenantInput input)
    {
        return await _sysTenantRep.AsQueryable()
            .LeftJoin<SysUser>((u, a) => u.UserId == a.Id).ClearFilter()
            .LeftJoin<SysOrg>((u, a, b) => u.OrgId == b.Id).ClearFilter()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Phone), (u, a) => a.Phone.Contains(input.Phone.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), (u, a, b) => b.Name.Contains(input.Name.Trim()))
            .WhereIF(!input.IncludeDefault, u => u.Id.ToString() != SqlSugarConst.MainConfigId) // 排除默认主库/主租户
            .OrderBy(u => new { u.OrderNo, u.Id })
            .Select((u, a, b) => new TenantOutput
            {
                Id = u.Id,
                OrgId = b.Id,
                OrgPid = b.Pid,
                Name = b.Name,
                UserId = a.Id,
                AdminAccount = a.Account,
                RealName = a.RealName,
                Phone = a.Phone,
                Email = a.Email,
                Host = u.Host,
                ExpirationTime = u.ExpirationTime,
                TenantType = u.TenantType,
                DbType = u.DbType,
                Connection = u.Connection,
                ConfigId = u.ConfigId,
                SlaveConnections = u.SlaveConnections,
                OrderNo = u.OrderNo,
                Remark = u.Remark,
                Status = u.Status,
                CreateTime = u.CreateTime,
                CreateUserName = u.CreateUserName,
                UpdateTime = u.UpdateTime,
                UpdateUserName = u.UpdateUserName,
            })
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取库隔离的租户列表
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public async Task<List<SysTenant>> GetTenantDbList()
    {
        return await _sysTenantRep.GetListAsync(u => u.TenantType == TenantTypeEnum.Db && u.Status == StatusEnum.Enable);
    }

    /// <summary>
    /// 增加租户 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加租户")]
    public async Task AddTenant(AddTenantInput input)
    {
        var isExist = await _sysOrgRep.IsAnyAsync(u => u.Name == input.Name);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1300);

        isExist = await _sysUserRep.AsQueryable().ClearFilter().AnyAsync(u => u.Account == input.AdminAccount);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1301);

        // 从库配置判断
        if (input.TenantType == TenantTypeEnum.Db && !string.IsNullOrWhiteSpace(input.SlaveConnections) && !JSON.IsValid(input.SlaveConnections, true))
            throw Oops.Oh(ErrorCodeEnum.D1302);

        switch (input.TenantType)
        {
            // Id隔离时设置与主库一致
            case TenantTypeEnum.Id:
                var config = _sysTenantRep.AsSugarClient().CurrentConnectionConfig;
                input.DbType = config.DbType;
                input.Connection = config.ConnectionString;
                break;

            case TenantTypeEnum.Db:
                if (string.IsNullOrWhiteSpace(input.Connection))
                    throw Oops.Oh(ErrorCodeEnum.Z1004);
                break;

            default:
                throw Oops.Oh(ErrorCodeEnum.D3004);
        }

        var tenant = input.Adapt<TenantOutput>();
        await _sysTenantRep.InsertAsync(tenant);
        await InitNewTenant(tenant);

        await CacheTenant();
    }

    /// <summary>
    /// 设置租户状态 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("设置租户状态")]
    public async Task<int> SetStatus(TenantInput input)
    {
        var tenant = await _sysTenantRep.GetByIdAsync(input.Id);
        if (tenant == null || tenant.ConfigId == SqlSugarConst.MainConfigId)
            throw Oops.Oh(ErrorCodeEnum.Z1001);

        if (!Enum.IsDefined(input.Status))
            throw Oops.Oh(ErrorCodeEnum.D3005);

        tenant.Status = input.Status;
        return await _sysTenantRep.AsUpdateable(tenant).UpdateColumns(u => new { u.Status }).ExecuteCommandAsync();
    }

    /// <summary>
    /// 新增租户初始化
    /// </summary>
    /// <param name="tenant"></param>
    private async Task InitNewTenant(TenantOutput tenant)
    {
        var tenantId = tenant.Id;
        var tenantName = tenant.Name;
        var tenantMark = tenant.Name + "-租管";

        // 初始化机构
        var newOrg = new SysOrg
        {
            Id = tenantId,
            TenantId = tenantId,
            Pid = tenant.OrgPid,
            Name = tenantName,
            Code = tenantName,
            Remark = tenantName,
        };
        await _sysOrgRep.InsertAsync(newOrg);

        // 初始化角色
        var newRole = new SysRole
        {
            Id = tenantId,
            TenantId = tenantId,
            Name = tenantMark,
            Code = CommonConst.SysAdminRole,
            DataScope = DataScopeEnum.All,
            Remark = tenantMark
        };
        await _sysRoleRep.InsertAsync(newRole);

        // 初始化职位
        var newPos = new SysPos
        {
            Id = tenantId,
            TenantId = tenantId,
            Name = tenantMark,
            Code = tenantName,
            Remark = tenantMark,
        };
        await _sysPosRep.InsertAsync(newPos);

        // 初始化系统账号
        var password = await _sysConfigService.GetConfigValueByCode<string>(ConfigConst.SysPassword);
        var newUser = new SysUser
        {
            Id = tenantId,
            TenantId = tenantId,
            Account = tenant.AdminAccount,
            Password = CryptogramHelper.Encrypt(password),
            RealName = tenant.RealName + "-租管",
            NickName = tenant.RealName + "-租管",
            AccountType = AccountTypeEnum.SysAdmin,
            Phone = tenant.Phone,
            Email = tenant.Email,
            OrgId = newOrg.Id,
            PosId = newPos.Id,
            Birthday = DateTime.Parse("2000-01-01"),
            Remark = tenantMark,
        };
        await _sysUserRep.InsertAsync(newUser);

        // 关联用户及角色
        var newUserRole = new SysUserRole
        {
            RoleId = newRole.Id,
            UserId = newUser.Id
        };
        await _userRoleRep.InsertAsync(newUserRole);

        // 关联租户组织机构和管理员用户
        await _sysTenantRep.UpdateAsync(u => new SysTenant() { UserId = newUser.Id, OrgId = newOrg.Id }, u => u.Id == tenantId);

        // 默认租户管理员角色菜单集合（工作台、账号管理、角色管理、机构管理、职位管理、个人中心、通知公告）
        var menuPidList = new List<long> { 1300000000101, 1310000000111, 1310000000131, 1310000000141, 1310000000151, 1310000000161, 1310000000171 };
        var menuIdList = await _sysMenuRep.AsQueryable().ClearFilter()
            .Where(u => menuPidList.Contains(u.Id) || menuPidList.Contains(u.Pid)).Select(u => u.Id).ToListAsync();
        await _sysRoleMenuService.GrantRoleMenu(new RoleMenuInput() { Id = newRole.Id, MenuIdList = menuIdList });

        // 发布新增租户事件
        await _eventPublisher.PublishAsync(TenantEventTypeEnum.Add, tenant);
    }

    /// <summary>
    /// 删除租户 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除租户")]
    public async Task DeleteTenant(DeleteTenantInput input)
    {
        // 禁止删除默认租户
        if (input.Id.ToString() == SqlSugarConst.MainConfigId)
            throw Oops.Oh(ErrorCodeEnum.D1023);

        // 若账号为开放接口绑定租户则禁止删除
        var isOpenAccessTenant = await _sysTenantRep.ChangeRepository<SqlSugarRepository<SysOpenAccess>>().IsAnyAsync(u => u.BindTenantId == input.Id);
        if (isOpenAccessTenant)
            throw Oops.Oh(ErrorCodeEnum.D1031);

        await _sysTenantRep.DeleteByIdAsync(input.Id);

        await CacheTenant(input.Id);

        // 删除与租户相关的表数据
        var users = await _sysUserRep.AsQueryable().ClearFilter().Where(u => u.TenantId == input.Id).ToListAsync();
        var userIds = users.Select(u => u.Id).ToList();
        await _sysUserRep.AsDeleteable().Where(u => userIds.Contains(u.Id)).ExecuteCommandAsync();

        await _userRoleRep.AsDeleteable().Where(u => userIds.Contains(u.UserId)).ExecuteCommandAsync();

        await _sysUserExtOrgRep.AsDeleteable().Where(u => userIds.Contains(u.UserId)).ExecuteCommandAsync();

        await _sysRoleRep.AsDeleteable().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();

        var roleIds = await _sysRoleRep.AsQueryable().ClearFilter().Where(u => u.TenantId == input.Id).Select(u => u.Id).ToListAsync();
        await _sysRoleMenuRep.AsDeleteable().Where(u => roleIds.Contains(u.RoleId)).ExecuteCommandAsync();

        await _sysOrgRep.AsDeleteable().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();

        await _sysPosRep.AsDeleteable().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();

        // 发布删除租户事件
        await _eventPublisher.PublishAsync(TenantEventTypeEnum.Delete, input);
    }

    /// <summary>
    /// 更新租户 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新租户")]
    public async Task UpdateTenant(UpdateTenantInput input)
    {
        var isExist = await _sysOrgRep.IsAnyAsync(u => u.Name == input.Name && u.Id != input.OrgId);
        if (isExist)
            throw Oops.Oh(ErrorCodeEnum.D1300);
        isExist = await _sysUserRep.IsAnyAsync(u => u.Account == input.AdminAccount && u.Id != input.UserId);
        if (isExist)
            throw Oops.Oh(ErrorCodeEnum.D1301);

        // Id隔离时设置与主库一致
        switch (input.TenantType)
        {
            case TenantTypeEnum.Id:
                var config = _sysTenantRep.AsSugarClient().CurrentConnectionConfig;
                input.DbType = config.DbType;
                input.Connection = config.ConnectionString;
                break;

            case TenantTypeEnum.Db:
                if (string.IsNullOrWhiteSpace(input.Connection))
                    throw Oops.Oh(ErrorCodeEnum.Z1004);
                break;

            default:
                throw Oops.Oh(ErrorCodeEnum.D3004);
        }

        // 从库配置判断
        if (input.TenantType == TenantTypeEnum.Db && !string.IsNullOrWhiteSpace(input.SlaveConnections) && !JSON.IsValid(input.SlaveConnections, true))
            throw Oops.Oh(ErrorCodeEnum.D1302);

        await _sysTenantRep.AsUpdateable(input.Adapt<TenantOutput>()).IgnoreColumns(true).ExecuteCommandAsync();

        // 更新系统机构
        await _sysOrgRep.UpdateAsync(u => new SysOrg() { Name = input.Name, Pid = input.OrgPid }, u => u.Id == input.OrgId);

        // 更新系统用户
        await _sysUserRep.UpdateAsync(u => new SysUser() { Account = input.AdminAccount, RealName = input.RealName, Phone = input.Phone, Email = input.Email },
            u => u.Id == input.UserId);

        await CacheTenant(input.Id);

        // 发布更新租户事件
        await _eventPublisher.PublishAsync(TenantEventTypeEnum.Update, input);
    }

    /// <summary>
    /// 授权租户管理员角色菜单 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("授权租户管理员角色菜单")]
    public async Task GrantMenu(RoleMenuInput input)
    {
        // 获取租户管理员角色【sys_admin】
        var adminRole = await _sysRoleRep.AsQueryable().ClearFilter()
            .FirstAsync(u => u.Code == CommonConst.SysAdminRole && u.TenantId == input.Id && u.IsDelete == false);
        if (adminRole == null) return;

        input.Id = adminRole.Id; // 重置租户管理员角色Id
        await _sysRoleMenuService.GrantRoleMenu(input);

        await _sysRoleService.ClearUserApiCache(input.Id);
    }

    /// <summary>
    /// 获取租户管理员角色拥有菜单Id集合 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取租户管理员角色拥有菜单Id集合")]
    public async Task<List<long>> GetOwnMenuList([FromQuery] TenantUserInput input)
    {
        var roleIds = await _sysUserRoleService.GetUserRoleIdList(input.UserId);
        return await _sysRoleMenuService.GetRoleMenuIdList([roleIds[0]]);
    }

    /// <summary>
    /// 重置租户管理员密码 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("重置租户管理员密码")]
    public async Task<string> ResetPwd(TenantUserInput input)
    {
        var password = await _sysConfigService.GetConfigValueByCode<string>(ConfigConst.SysPassword);
        var encryptPassword = CryptogramHelper.Encrypt(password);
        await _sysUserRep.UpdateAsync(u => new SysUser() { Password = encryptPassword }, u => u.Id == input.UserId);
        return password;
    }

    /// <summary>
    /// 同步所有租户数据库 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("同步所有租户数据库")]
    public async Task SyncTenantDb()
    {
        var tenantList = await _sysTenantRep.GetListAsync();
        foreach (var tenant in tenantList)
        {
            await InitTenantDb(new TenantInput { Id = tenant.Id });
        }
    }

    /// <summary>
    /// 缓存所有租户
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    [NonAction]
    public async Task CacheTenant(long tenantId = 0)
    {
        // 移除 ISqlSugarClient 中的库连接并排除默认主库
        if (tenantId > 0 && tenantId.ToString() != SqlSugarConst.MainConfigId)
            _sysTenantRep.AsTenant().RemoveConnection(tenantId);

        var tenantList = await _sysTenantRep.GetListAsync();
        // 对租户库连接进行SM2加密
        foreach (var tenant in tenantList)
        {
            if (!string.IsNullOrWhiteSpace(tenant.Connection))
                tenant.Connection = CryptogramHelper.SM2Encrypt(tenant.Connection);
        }

        _sysCacheService.Set(CacheConst.KeyTenant, tenantList);
    }

    /// <summary>
    /// 创建租户数据库 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("创建租户数据库")]
    public async Task InitTenantDb(TenantInput input)
    {
        var tenant = await _sysTenantRep.GetByIdAsync(input.Id);
        if (tenant == null) return;

        if (tenant.DbType == SqlSugar.DbType.Oracle)
            throw Oops.Oh(ErrorCodeEnum.Z1002);

        if (string.IsNullOrWhiteSpace(tenant.Connection) || tenant.Connection.Length < 10)
            throw Oops.Oh(ErrorCodeEnum.Z1004);

        // 默认数据库配置
        var defaultConfig = App.GetOptions<DbConnectionOptions>().ConnectionConfigs.FirstOrDefault();
        var tenantConnConfig = new DbConnectionConfig
        {
            ConfigId = tenant.Id.ToString(),
            DbType = tenant.DbType,
            IsAutoCloseConnection = true,
            ConnectionString = tenant.Connection,
            DbSettings = new DbSettings()
            {
                EnableInitDb = true,
                EnableDiffLog = false,
                EnableUnderLine = defaultConfig.DbSettings.EnableUnderLine,
            },
            //SlaveConnectionConfigs = JSON.IsValid(tenant.SlaveConnections) ? JSON.Deserialize<List<SlaveConnectionConfig>>(tenant.SlaveConnections) : null // 从库连接配置
        };
        SqlSugarSetup.InitTenantDatabase(tenantConnConfig);
    }

    /// <summary>
    /// 创建租户数据 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("创建租户数据")]
    public async Task InitTenantData(TenantInput input)
    {
        var tenant = await _sysTenantRep.GetByIdAsync(input.Id);
        if (tenant == null) return;

        if (string.IsNullOrWhiteSpace(tenant.Connection) || tenant.Connection.Length < 10)
            throw Oops.Oh(ErrorCodeEnum.Z1004);

        SqlSugarSetup.InitTenantData(_sysTenantRep.AsTenant(), SqlSugarConst.MainConfigId.ToLong(), tenant.Id);
    }

    /// <summary>
    /// 获取租户下的用户列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取租户下的用户列表")]
    public async Task<List<SysUser>> UserList(TenantIdInput input)
    {
        return await _sysUserRep.AsQueryable().ClearFilter().Where(u => u.TenantId == input.TenantId).ToListAsync();
    }

    /// <summary>
    /// 获取租户数据库连接
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public SqlSugarScopeProvider GetTenantDbConnectionScope(long tenantId)
    {
        var iTenant = _sysTenantRep.AsTenant();

        // 若已存在租户库连接，则直接返回
        if (iTenant.IsAnyConnection(tenantId.ToString()))
            return iTenant.GetConnectionScope(tenantId.ToString());

        lock (iTenant)
        {
            // 从缓存里面获取租户信息
            var tenant = _sysCacheService.Get<List<SysTenant>>(CacheConst.KeyTenant)?.FirstOrDefault(u => u.Id == tenantId);
            if (tenant == null || tenant.TenantType == TenantTypeEnum.Id) return null;

            // 获取默认库连接配置
            var dbOptions = App.GetOptions<DbConnectionOptions>();
            var mainConnConfig = dbOptions.ConnectionConfigs.First(u => u.ConfigId.ToString() == SqlSugarConst.MainConfigId);

            // 设置租户库连接配置
            var tenantConnConfig = new DbConnectionConfig
            {
                ConfigId = tenant.Id.ToString(),
                DbType = tenant.DbType,
                IsAutoCloseConnection = true,
                ConnectionString = CryptogramHelper.SM2Decrypt(tenant.Connection), // 对租户库连接进行SM2解密
                DbSettings = new DbSettings()
                {
                    EnableUnderLine = mainConnConfig.DbSettings.EnableUnderLine,
                },
                SlaveConnectionConfigs = JSON.IsValid(tenant.SlaveConnections) ? JSON.Deserialize<List<SlaveConnectionConfig>>(tenant.SlaveConnections) : null // 从库连接配置
            };
            iTenant.AddConnection(tenantConnConfig);

            var sqlSugarScopeProvider = iTenant.GetConnectionScope(tenantId.ToString());
            SqlSugarSetup.SetDbConfig(tenantConnConfig);
            SqlSugarSetup.SetDbAop(sqlSugarScopeProvider, dbOptions.EnableConsoleSql);

            return sqlSugarScopeProvider;
        }
    }

    /// <summary>
    /// 获取系统信息 🔖
    /// </summary>
    /// <returns></returns>
    [SuppressMonitor]
    [AllowAnonymous]
    [DisplayName("获取系统信息")]
    public async Task<dynamic> GetSysInfo(long tenantId)
    {
        //// 还可以根据域名判断租户
        //var host = App.HttpContext.Request.Host.ToString();

        if (tenantId < 1) tenantId = long.Parse(App.User?.FindFirst(ClaimConst.TenantId)?.Value ?? "0");
        if (tenantId < 1) tenantId = SqlSugarConst.DefaultTenantId;
        var tenant = await _sysTenantRep.GetFirstAsync(u => u.Id == tenantId);
        if (tenant == null) return "";

        // 若租户系统标题为空，则获取默认租户系统信息（兼容已有未配置的租户）
        if (string.IsNullOrWhiteSpace(tenant.Title))
            tenant = await _sysTenantRep.GetFirstAsync(u => u.Id == SqlSugarConst.DefaultTenantId);

        //// 获取首页轮播图列表
        //var carouselFiles = await _fileRep.GetListAsync(u => u.BelongId == tenant.Id && u.RelationId == tenant.Id && u.FileType == "Carousel");

        var forceChangePassword = await _sysConfigService.GetConfigValueByCode<bool>(ConfigConst.SysForceChangePassword); // 强制修改密码
        var passwordExpirationTime = await _sysConfigService.GetConfigValueByCode<int>(ConfigConst.SysPasswordExpirationTime); // 密码有效期
        var i18NSwitch = await _sysConfigService.GetConfigValueByCode<bool>(ConfigConst.SysI18NSwitch); // 开启多语言切换
        var idleTimeout = await _sysConfigService.GetConfigValueByCode<int>(ConfigConst.SysIdleTimeout); // 闲置超时时间
        var onlineNotice = await _sysConfigService.GetConfigValueByCode<bool>(ConfigConst.SysOnlineNotice); // 上线下线通知
        var publicKey = App.GetConfig<string>("Cryptogram:PublicKey", true); // 获取密码加解密公钥配置

        return new
        {
            TenantId = tenantId,
            tenant.Logo,
            tenant.Title,
            tenant.ViceTitle,
            tenant.ViceDesc,
            tenant.Copyright,
            tenant.Icp,
            tenant.IcpUrl,
            tenant.Watermark,
            tenant.Version,
            tenant.ThemeColor,
            tenant.Layout,
            tenant.Animation,
            tenant.Captcha,
            tenant.SecondVer,
            ForceChangePassword = forceChangePassword,
            PasswordExpirationTime = passwordExpirationTime,
            PublicKey = publicKey,
            //CarouselFiles = carouselFiles,
            I18NSwitch = i18NSwitch,
            IdleTimeout = idleTimeout,
            OnlineNotice = onlineNotice,
        };
    }

    /// <summary>
    /// 保存系统信息 🔖
    /// </summary>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("保存系统信息")]
    public async Task SaveSysInfo(SysInfoInput input)
    {
        if (input.TenantId < 1)
            input.TenantId = long.Parse(App.User?.FindFirst(ClaimConst.TenantId)?.Value ?? "0");
        var tenant = await _sysTenantRep.GetFirstAsync(u => u.Id == input.TenantId);
        _ = tenant ?? throw Oops.Oh(ErrorCodeEnum.D1002);

        var originLogo = tenant.Logo;
        tenant = input.Adapt<SysTenant>();
        tenant.Id = input.TenantId;

        // logo 不为空才保存
        if (!string.IsNullOrEmpty(input.LogoBase64))
        {
            // 旧图标文件相对路径
            var oldSysLogoRelativeFilePath = tenant.Logo ?? "";
            var oldSysLogoAbsoluteFilePath = Path.Combine(App.WebHostEnvironment.WebRootPath, oldSysLogoRelativeFilePath.TrimStart('/'));

            var groups = Regex.Match(input.LogoBase64, @"data:image/(?<type>.+?);base64,(?<data>.+)").Groups;
            //var type = groups["type"].Value;
            var base64Data = groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            // 根据文件名取扩展名
            var ext = string.IsNullOrWhiteSpace(input.LogoFileName) ? ".png" : Path.GetExtension(input.LogoFileName);
            // 本地图标保存路径
            var path = $"upload/{input.TenantId}/";
            var fileName = $"logo{ext}".ToLower();
            var absoluteFilePath = Path.Combine(App.WebHostEnvironment.WebRootPath, path, fileName);

            // 删除已存在文件
            if (File.Exists(oldSysLogoAbsoluteFilePath))
                File.Delete(oldSysLogoAbsoluteFilePath);

            // 创建文件夹
            var absoluteFileDir = Path.GetDirectoryName(absoluteFilePath);
            if (!Directory.Exists(absoluteFileDir))
                Directory.CreateDirectory(absoluteFileDir);

            // 保存图标文件
            await File.WriteAllBytesAsync(absoluteFilePath, binData);

            // 保存图标配置
            tenant.Logo = $"/{path}/{fileName}";
        }
        else
        {
            tenant.Logo = originLogo;
        }

        await _sysTenantRep.AsUpdateable(tenant).UpdateColumns(u => new
        {
            u.Logo,
            u.Title,
            u.ViceTitle,
            u.ViceDesc,
            u.Copyright,
            u.Icp,
            u.IcpUrl,
            u.Watermark,
            u.Version,
            u.ThemeColor,
            u.Layout,
            u.Animation,
            u.Captcha,
            u.SecondVer
        }).ExecuteCommandAsync();

        // 更新租户缓存
        await CacheTenant();
    }

    /// <summary>
    /// 上传轮播图单文件 🔖
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [DisplayName("上传轮播图单文件")]
    public async Task<SysFile> UploadCarouselFile([Required] IFormFile file)
    {
        var tenantId = long.Parse(App.User?.FindFirst(ClaimConst.TenantId)?.Value ?? "0");
        if (tenantId < 1) tenantId = SqlSugarConst.DefaultTenantId;
        var tenant = await _sysTenantRep.GetFirstAsync(u => u.Id == tenantId);
        if (tenant == null) return null;

        if (file == null)
            throw Oops.Oh(ErrorCodeEnum.D8000);

        // 本地轮播图保存路径
        var path = $"upload/{tenantId}/carousel";
        var absoluteDirPath = Path.Combine(App.WebHostEnvironment.WebRootPath, path);

        // 创建文件夹
        if (!Directory.Exists(absoluteDirPath))
            Directory.CreateDirectory(absoluteDirPath);

        // 保存轮播图文件
        var sysFile = await _sysFileService.UploadFile(new UploadFileInput { File = file, FileType = "Carousel" });

        //// 保存轮播图配置
        //sysFile.BelongId = tenant.Id;
        //sysFile.RelationId = tenant.Id;

        await _sysFileService.UpdateFile(sysFile);

        return sysFile;
    }
}