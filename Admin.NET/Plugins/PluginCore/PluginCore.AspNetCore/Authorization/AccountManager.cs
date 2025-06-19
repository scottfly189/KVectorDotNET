// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using PluginCore.Utils;

namespace PluginCore.AspNetCore.Authorization
{
    public class AccountManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Microsoft.AspNetCore.Http.HttpContext HttpContext
        {
            get
            {
                return this._httpContextAccessor.HttpContext;
            }
        }

        public AccountManager(IHttpContextAccessor httpContextAccessor)
        {
            // Exception: IFeatureCollection has been disposed. Object name: 'Collection'.
            // https://stackoverflow.com/questions/59963383/session-setstring-method-throws-exception-ifeaturecollection-has-been-disposed
            //HttpContext = ((HttpContextAccessor)httpContextAccessor).HttpContext;
            //HttpContext = httpContextAccessor.HttpContext;
            // 注意: 不要将 HttpContext 保存起来，应当每次都从 httpContextAccessor 取
            _httpContextAccessor = httpContextAccessor;
        }

        public static Config.PluginCoreConfig.AdminModel Admin
        {
            get
            {
                return Config.PluginCoreConfigFactory.Create().Admin;
            }
            set
            {
                var sourceModel = Config.PluginCoreConfigFactory.Create();
                sourceModel.Admin = value;
                Config.PluginCoreConfigFactory.Save(sourceModel);
            }
        }

        public static string CurrentToken(HttpContext httpContext)
        {
            string token = null;
            HttpRequest request = httpContext.Request;
            try
            {
                // header -> cookie
                try
                {
                    // header 中找 token
                    if (request.Headers.ContainsKey("Authorization"))
                    {
                        string authHeader = request.Headers["Authorization"];
                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer"))
                        {
                            token = authHeader.Substring("Bearer ".Length).Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
                if (string.IsNullOrEmpty(token))
                {
                    // cookie 中找 token
                    //string tokenCookieName = "token";
                    // string tokenCookieName = "PluginCore.Admin.Token";
                    string tokenCookieName = IPlugins.Constants.AspNetCoreAuthorizationTokenCookieName;
                    if (request.Cookies.Keys.Contains(tokenCookieName))
                    {
                        if (request.Cookies[tokenCookieName] != null && string.IsNullOrEmpty(request.Cookies[tokenCookieName]) == false)
                        {
                            token = request.Cookies[tokenCookieName];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return token;
        }

        public string CurrentToken()
        {
            return CurrentToken(this.HttpContext);
        }

        public static string CreateToken()
        {
            return CreateToken(Admin.UserName, Admin.Password);
        }

        public static string CreateToken(string userName, string password)
        {
            string token = $"UserName={userName}&Password={password}";
            token = Md5Helper.MD5Encrypt32(token);

            return token;
        }

        public static bool IsAdminToken(string token)
        {
            bool isAdmin = false;
            isAdmin = CreateToken().Equals(token);

            return isAdmin;
        }

        public bool IsAdmin()
        {
            return IsAdmin(this.HttpContext);
        }

        public static bool IsAdmin(HttpContext httpContext)
        {
            bool isAdmin = false;
            try
            {
                string currentToken = CurrentToken(httpContext);
                isAdmin = IsAdminToken(currentToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isAdmin;
        }
    }
}


