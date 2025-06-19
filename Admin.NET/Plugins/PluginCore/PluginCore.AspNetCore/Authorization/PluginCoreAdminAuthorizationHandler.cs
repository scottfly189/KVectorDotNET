// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PluginCore.AspNetCore.Authorization
{
    public class PluginCoreAdminAuthorizationHandler : AuthorizationHandler<PluginCoreAdminRequirement>
    {
        private readonly AccountManager _accountManager;

        public PluginCoreAdminAuthorizationHandler(AccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        /// <summary>
        /// 必须在其中呼叫一次 <see cref="AuthorizationHandlerContext.Succeed(IAuthorizationRequirement)"/> 代表满足 <see cref="PluginCoreAdminRequirement"/>，否则皆为 不满足此 Requirement
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PluginCoreAdminRequirement requirement)
        {
            bool isAdmin = this._accountManager.IsAdmin();
            if (!isAdmin)
            {
                context.Fail();
            }
            else
            {
                // 认证通过后, 可通过下面方式获取 token
                var identity = context.User.Identity;

                string token = this._accountManager.CurrentToken();

                // Utils.LogUtil.Info<PluginCoreAdminAuthorizationHandler>($"通过 Authorization: token: {token}");
                Utils.LogUtil.Info<PluginCoreAdminAuthorizationHandler>($"Authorization Granted");

                context.Succeed(requirement);
            }

            await Task.CompletedTask;
        }
    }
}


