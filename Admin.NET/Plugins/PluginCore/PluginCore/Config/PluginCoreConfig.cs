// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Config
{
    public class PluginCoreConfig
    {
        public PluginCoreConfig()
        {
            this.Admin = new AdminModel();
        }

        public AdminModel Admin { get; set; }

        /// <summary>
        /// LocalEmbedded
        /// LocalFolder
        /// RemoteCDN
        /// </summary>
        public string FrontendMode { get; set; } = "LocalEmbedded";

        public string RemoteFrontend { get; set; } = "https://cdn.jsdelivr.net/gh/yiyungent/plugincore-admin-frontend@0.3.1/dist-cdn";

        /// <summary>
        /// 开启后:
        /// 1. 页面的 Widget 会显示插件的详细插入点
        /// </summary>
        public bool PluginWidgetDebug { get; set; } = false;

        public sealed class AdminModel
        {
            public string UserName { get; set; } = "admin";

            public string Password { get; set; } = "ABC12345";
        }

    }
}


