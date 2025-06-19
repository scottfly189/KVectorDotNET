// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using PluginCore.Models;

namespace PluginCore
{
    /// <summary>
    /// TODO: 目前这样读取出来的包含了 windows 换行符 \r\n
    /// </summary>
    public class PluginReadmeModelFactory
    {
        // TODO: Linux 文件名下区分大小写, windows不区分, 目前必须为 README.md
        private const string ReadmeFile = "README.md";

        #region 即时读取
        public static PluginReadmeModel Create(string pluginId)
        {
            PluginReadmeModel readmeModel = new PluginReadmeModel();
            string pluginDir = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId);
            string pluginReadmeFilePath = Path.Combine(pluginDir, ReadmeFile);

            if (!File.Exists(pluginReadmeFilePath))
            {
                return null;
            }
            try
            {
                string readmeStr = File.ReadAllText(pluginReadmeFilePath, Encoding.UTF8);
                readmeModel.PluginId = pluginId;
                readmeModel.Content = readmeStr;
            }
            catch (Exception ex)
            {
                readmeModel = null;
            }

            return readmeModel;
        }
        #endregion
    }
}


