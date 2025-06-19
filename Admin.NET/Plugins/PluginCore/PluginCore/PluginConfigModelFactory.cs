// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Linq;
//===================================================
//  License: GNU LGPLv3
//  Contributors: yiyungent@gmail.com
//  Project: https://yiyungent.github.io/PluginCore
//  GitHub: https://github.com/yiyungent/PluginCore
//===================================================



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using PluginCore.Models;

namespace PluginCore
{
    public class PluginConfigModelFactory
    {
        #region 即时读取
        public static PluginConfigModel Create()
        {
            PluginConfigModel pluginConfigModel = new PluginConfigModel();
            string pluginConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "plugin.config.json");
            string pluginConfigJsonStr = File.ReadAllText(pluginConfigFilePath, Encoding.UTF8);
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            pluginConfigModel = JsonSerializer.Deserialize<PluginConfigModel>(pluginConfigJsonStr, jsonSerializerOptions);
            pluginConfigModel = EnabledPluginsSort(pluginConfigModel);

            return pluginConfigModel;
        } 
        #endregion

        #region 保存
        public static void Save(PluginConfigModel pluginConfigModel)
        {
            if (pluginConfigModel == null)
            {
                throw new ArgumentNullException(nameof(pluginConfigModel));
            }
            try
            {
                pluginConfigModel = EnabledPluginsSort(pluginConfigModel);
                string pluginConfigJsonStr = JsonSerializer.Serialize(pluginConfigModel);
                string pluginConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "plugin.config.json");
                File.WriteAllText(pluginConfigFilePath, pluginConfigJsonStr, Encoding.UTF8);
            }
            catch (Exception ex)
            { }

        } 
        #endregion

        #region 确保建立正确的依赖顺序
        public static PluginConfigModel EnabledPluginsSort(PluginConfigModel pluginConfigModel) {
            var dependencySorter = new PluginCore.Utils.DependencySorter<string>();
            dependencySorter.AddObjects(pluginConfigModel.EnabledPlugins.ToArray());
            foreach (var plugin in pluginConfigModel.EnabledPlugins)
            {
                try
                {
                    IList<string> dependPlugins = PluginInfoModelFactory.Create(plugin).DependPlugins;
                    if (dependPlugins != null && dependPlugins.Count >= 1) {
                        dependencySorter.SetDependencies(obj: plugin, dependsOnObjects: dependPlugins.ToArray());
                    }
                }
                catch (System.Exception ex)
                {
                }
            }
            try
            {
                var sortedPlugins = dependencySorter.Sort(); 
                if (sortedPlugins != null && sortedPlugins.Length >= 1) {
                    pluginConfigModel.EnabledPlugins = sortedPlugins.ToList();
                }
            }
            catch (System.Exception ex)
            {
            }

            return pluginConfigModel;
        }
        #endregion
    }
}


