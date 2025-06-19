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
    /// 所有插件的配置信息模型
    /// <para>对应 WebApi/App_Data/plugin.config.json</para>
    /// <para>-------------</para>
    /// <para> Plugins = 已启用 + 已禁用 </para>
    /// <para> 上传放入 Plugins 后, 默认为 已禁用 </para>
    /// </summary>
    public class PluginConfigModel
    {
        /// <summary>
        /// 启用的插件列表: PluginID
        /// <para>属于 插件 已安装</para>
        /// </summary>
        public List<string> EnabledPlugins { get; set; }

        #region ctor
        public PluginConfigModel()
        {
            this.EnabledPlugins = new List<string>();
        }
        #endregion
    }
}


