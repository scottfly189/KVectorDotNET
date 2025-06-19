// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PluginCore.Models
{
    /// <summary>
    /// 插件信息模型
    /// <para>对应插件目录下 info.json</para>
    /// <para>约定: 插件文件夹名=PluginID</para>
    /// <para>约定: 插件文件夹名=插件主程序集(Assembly)名 .dll</para>
    /// <para>eg: plugins/payment  payment.dll</para>
    /// <para>约定: 插件文件夹下 logo.png 为插件图标</para>
    /// <para>约定: 插件文件夹下 README.md 为插件说明文件</para>
    /// <para>约定: 插件文件夹下 settings.json 为插件设置文件</para>
    /// </summary>
    public class PluginInfoModel
    {
        public string PluginId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Version { get; set; }

        public IList<string> SupportedVersions { get; set; }

        /// <summary>
        /// 前置依赖插件
        /// </summary>
        /// <value></value>
        public IList<string> DependPlugins { get; set; }

        #region Ctor
        public PluginInfoModel()
        {
            this.SupportedVersions = new List<string>();
            this.DependPlugins = new List<string>();
        }
        #endregion
    }
}


