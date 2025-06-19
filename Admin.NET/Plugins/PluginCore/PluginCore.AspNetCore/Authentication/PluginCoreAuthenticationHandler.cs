// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PluginCore.AspNetCore.Authorization;

namespace PluginCore.AspNetCore.Authentication
{
    /// <summary>
    /// https://stackoverflow.com/questions/52287542/invalidoperationexception-no-authenticationscheme-was-specified-and-there-was
    /// </summary>
    public class PluginCoreAuthenticationHandler : AuthenticationHandler<PluginCoreAuthenticationSchemeOptions>
    {
        private readonly AccountManager _accountManager;

        public PluginCoreAuthenticationHandler(IOptionsMonitor<PluginCoreAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, AccountManager accountManager) : base(options, logger, encoder, clock)
        {
            this._accountManager = accountManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string token = this._accountManager.CurrentToken();
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.NoResult();
            }

            bool isAdmin = AccountManager.IsAdminToken(token);

            if (!isAdmin)
            {
                return AuthenticateResult.Fail($"token is not admin");
            }
            else
            {
                var id = new ClaimsIdentity(
                    // new Claim[] { new Claim("PluginCore.Token", token) },  // not safe , just as an example , should custom claims on your own
                    claims: new Claim[] { new Claim(type: IPlugins.Constants.AspNetCoreAuthenticationClaimType, value: token) },  // not safe , just as an example , should custom claims on your own
                    authenticationType: Scheme.Name
                );
                ClaimsPrincipal principal = new ClaimsPrincipal(identity: id);
                var ticket = new AuthenticationTicket(
                    principal: principal,
                    properties: new AuthenticationProperties(),
                    authenticationScheme: Scheme.Name);

                // Utils.LogUtil.Info<PluginCoreAuthenticationHandler>($"通过 Authentication: token: {token}");
                Utils.LogUtil.Info<PluginCoreAuthenticationHandler>($"Authentication Passed");

                return AuthenticateResult.Success(ticket);
            }

        }
    }
}


