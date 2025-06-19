// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace PluginCore.AspNetCore.AdminUI
{
    public class PluginCoreAdminUIOptions
    {
        /// <summary>
        /// Gets or sets a route prefix for accessing the swagger-ui
        /// </summary>
        public string RoutePrefix { get; set; } = "PluginCore/Admin";

        /// <summary>
        /// Gets or sets a Stream function for retrieving the swagger-ui page
        /// </summary>
        public Func<Stream> IndexStream
        {
            get
            {
                Func<Stream> funcStream = null;
                ;
                Config.PluginCoreConfig pluginCoreConfig = Config.PluginCoreConfigFactory.Create();
                switch (pluginCoreConfig.FrontendMode?.ToLower())
                {
                    case "localembedded":
                        funcStream = () => typeof(PluginCoreAdminUIOptions).GetTypeInfo().Assembly
                            .GetManifestResourceStream("PluginCore.AspNetCore.node_modules.plugincore_admin_frontend.dist.index.html");
                        break;
                    case "localfolder":
                        string absoluteRootPath = PluginPathProvider.PluginCoreAdminDir();
                        string indexFilePath = Path.Combine(absoluteRootPath, "index.html");

                        funcStream = () => (Stream)new FileStream(indexFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1, FileOptions.Asynchronous | FileOptions.SequentialScan);
                        break;
                    case "remotecdn":
                        string remoteFrontendRootPath = pluginCoreConfig.RemoteFrontend;
                        string indexFileRemotePath = remoteFrontendRootPath + "/" + "index.html";

                        funcStream = () => new HttpClient().GetStreamAsync(indexFileRemotePath).Result;
                        break;
                    default:
                        funcStream = () => typeof(PluginCoreAdminUIOptions).GetTypeInfo().Assembly
                             .GetManifestResourceStream("PluginCore.AspNetCore.node_modules.plugincore_admin_frontend.dist.index.html");
                        break;
                }

                return funcStream;
            }
        }


    }
}


