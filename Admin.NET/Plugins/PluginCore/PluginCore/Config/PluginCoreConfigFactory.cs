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

namespace PluginCore.Config
{
    public class PluginCoreConfigFactory
    {
        private const string FileName = "PluginCore.Config.json";

        #region 即时读取
        public static PluginCoreConfig Create()
        {
            PluginCoreConfig pluginCoreConfig = new PluginCoreConfig();
            string pluginCoreConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", FileName);
            string pluginCoreConfigJsonStr = string.Empty;
            if (!File.Exists(pluginCoreConfigFilePath))
            {
                // 不存在, 则新建初始化默认
                pluginCoreConfigJsonStr = JsonSerializer.Serialize(pluginCoreConfig);
                File.WriteAllText(pluginCoreConfigFilePath, pluginCoreConfigJsonStr, Encoding.UTF8);

                return pluginCoreConfig;
            }

            pluginCoreConfigJsonStr = File.ReadAllText(pluginCoreConfigFilePath, Encoding.UTF8);
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            pluginCoreConfig = JsonSerializer.Deserialize<PluginCoreConfig>(pluginCoreConfigJsonStr, jsonSerializerOptions);

            return pluginCoreConfig;
        }
        #endregion

        #region 保存
        public static void Save(PluginCoreConfig pluginCoreConfig)
        {
            if (pluginCoreConfig == null)
            {
                throw new ArgumentNullException(nameof(pluginCoreConfig));
            }
            try
            {
                string pluginCoreConfigJsonStr = JsonSerializer.Serialize(pluginCoreConfig);
                string pluginCoreConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", FileName);
                File.WriteAllText(pluginCoreConfigFilePath, pluginCoreConfigJsonStr, Encoding.UTF8);
            }
            catch (Exception ex)
            { }

        }
        #endregion


    }
}


