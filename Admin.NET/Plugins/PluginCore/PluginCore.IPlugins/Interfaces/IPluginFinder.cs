// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Text;
using PluginCore.IPlugins;

namespace PluginCore.Interfaces
{
    public interface IPluginFinder
    {
        #region 实现了指定接口或类型 的启用插件

        /// <summary>
        /// 实现了指定接口或类型 的启用插件
        /// </summary>
        /// <typeparam name="TPlugin">可以是一个接口，一个抽象类，一个普通实现类, 只要实现了 <see cref="IPlugin"/>即可</typeparam>
        /// <returns></returns>
        IEnumerable<TPlugin> EnablePlugins<TPlugin>()
           where TPlugin : IPlugin; // BasePlugin

        /// <summary>
        /// 实现了指定接口或类型 的启用插件
        /// </summary>
        /// <typeparam name="TPlugin">可以是一个接口，一个抽象类，一个普通实现类, 只要实现了 <see cref="IPlugin"/>即可</typeparam>
        /// <returns></returns>
        IEnumerable<(TPlugin PluginInstance, string PluginId)> EnablePluginsFull<TPlugin>()
            where TPlugin : IPlugin; // BasePlugin

        /// <summary>
        /// 所有启用的插件 的 PluginId 
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> EnablePluginIds<TPlugin>()
            where TPlugin : IPlugin; // BasePlugin
        #endregion

        #region 所有启用的插件

        /// <summary>
        /// 所有启用的插件
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPlugin> EnablePlugins();

        /// <summary>
        /// 所有启用的插件
        /// </summary>
        /// <returns></returns>
        IEnumerable<(IPlugin PluginInstance, string PluginId)> EnablePluginsFull();

         /// <summary>
        /// 所有启用的插件 的 PluginId 
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> EnablePluginIds();
        #endregion

        #region 获取指定 pluginId 的启用插件

        /// <summary>
        /// 获取指定 pluginId 的启用插件
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns>1.插件未启用返回null, 2.找不到此插件上下文返回null 3.找不到插件主dll返回null 4.插件主dll中找不到实现了IPlugin的Type返回null, 5.无法实例化插件返回null</returns>
        IPlugin Plugin(string pluginId);
        #endregion

    }
}


