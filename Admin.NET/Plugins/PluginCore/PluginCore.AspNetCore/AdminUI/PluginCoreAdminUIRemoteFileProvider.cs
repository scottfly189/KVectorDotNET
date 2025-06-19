// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace PluginCore.AspNetCore.AdminUI
{
    public class PluginCoreAdminUIRemoteFileProvider : IFileProvider
    {
        protected string RootUrl { get; set; }

        public PluginCoreAdminUIRemoteFileProvider(string rootUrl)
        {
            this.RootUrl = rootUrl;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return (IDirectoryContents)NotFoundDirectoryContents.Singleton;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (string.IsNullOrEmpty(subpath))
                return (IFileInfo)new NotFoundFileInfo(subpath);

            IFileInfo fileInfo = new PluginCoreAdminUIFileInfo(this.RootUrl, subpath);

            return fileInfo;
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }


        public class PluginCoreAdminUIFileInfo : IFileInfo
        {
            protected string RootUrl { get; set; }

            protected string SubPath { get; set; }

            private string _name;

            public PluginCoreAdminUIFileInfo(string rootUrl, string subpath)
            {
                this.RootUrl = rootUrl;
                this.SubPath = subpath;
                this._name = this.SubPath.Substring(this.SubPath.LastIndexOf("/") + 1);
            }

            public Stream CreateReadStream()
            {
                HttpClient httpClient = new HttpClient();

                return httpClient.GetStreamAsync($"{this.RootUrl}/{this.SubPath}").Result;
            }

            public bool Exists
            {
                get
                {
                    bool isExist = false;
                    if (this.Name == "index.html")
                    {
                        isExist = true;
                    }

                    return isExist;
                }
            }
            public bool IsDirectory
            {
                get
                {
                    return false;
                }
            }

            public DateTimeOffset LastModified
            {
                get
                {
                    return new DateTimeOffset(DateTime.Now);
                }
            }

            public long Length
            {
                get
                {
                    return 111;
                }
            }

            public string Name
            {
                get
                {
                    return this._name;
                }
            }

            public string PhysicalPath
            {
                get
                {
                    return "";
                }
            }
        }

    }



}


