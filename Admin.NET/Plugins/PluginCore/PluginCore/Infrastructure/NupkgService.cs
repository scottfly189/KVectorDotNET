// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PluginCore.Infrastructure
{
    public class NupkgService
    {
        /// <summary>
        /// 将 xxx.nupkg 根据当前环境 解压成 对应 插件目录结构
        /// xxx.zip 形式的插件安装包: 直接解压即可
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationDirectory"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool DecomparessFile(string sourceFile, string destinationDirectory = null)
        {
            bool isDecomparessSuccess = false;

            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException("要解压的文件不存在", sourceFile);
            }

            if (string.IsNullOrWhiteSpace(destinationDirectory))
            {
                destinationDirectory = Path.GetDirectoryName(sourceFile);
            }

            try
            {
                // 注意: 之前重命名过: Guid.zip
                destinationDirectory = destinationDirectory.Replace(".zip", "");

                isDecomparessSuccess = Utils.ZipHelper.FastDecomparessFile(sourceFile, destinationDirectory);
                if (!isDecomparessSuccess)
                {
                    return isDecomparessSuccess;
                }
                // 到这里已经解压完成, 开始解析

                var netVersion = Utils.RuntimeUtil.RuntimeNetVersion;

                // netcoreapp3.1  net5.0
                // dll 文件等
                string libDirPath = Path.Combine(destinationDirectory, "lib");
                string netFolderName = string.Empty;
                if (netVersion >= new Version("3.1") && Directory.Exists(Path.Combine(libDirPath, $"netcoreapp{netVersion.Major}.{netVersion.Minor}")))
                {
                    netFolderName = $"netcoreapp{netVersion.Major}.{netVersion.Minor}";
                }
                else if (netVersion.Major >= 5 && Directory.Exists(Path.Combine(libDirPath, $"net{netVersion.Major}.{netVersion.Minor}")))
                {
                    netFolderName = $"net{netVersion.Major}.{netVersion.Minor}";
                }
                else if (Directory.Exists(Path.Combine(libDirPath, "netstandard2.0")))
                {
                    netFolderName = $"netstandard2.0";
                }
                else if (Directory.Exists(Path.Combine(libDirPath, "netstandard2.1")))
                {
                    netFolderName = $"netstandard2.1";
                }
                else
                {
                    throw new Exception("暂不支持 .NET Core 3.1 以下版本, 也不支持 .NET Framework ");
                }
                // 1. ./lib/netcoreapp3.1
                string libNetDirPath = Path.Combine(libDirPath, netFolderName);
                // 移动到插件根目录
                //Directory.Move(libNetDirPath, destinationDirectory); // 错误: 这样移动会导致 包含根目录文件夹名
                Utils.FileUtil.CopyFolder(libNetDirPath, destinationDirectory);
                // 只需要这些, 其他删除
                Directory.Delete(libDirPath, true);

                Utils.LogUtil.Info<NupkgService>($"加载 nupkg 中 dll: {sourceFile} ; {libNetDirPath}");

                // 2. 普通文件: 例如 wwwroot
                // 2.1 ./content
                string contentDirPath = Path.Combine(destinationDirectory, "content");
                bool isExistContentDir = Directory.Exists(contentDirPath);
                bool isFinishedContent = false;
                if (isExistContentDir)
                {
                    DirectoryInfo dir = new DirectoryInfo(contentDirPath);
                    bool isExistFile = dir.GetFiles()?.Length >= 1 || dir.GetDirectories()?.Length >= 1;
                    if (isExistFile)
                    {
                        Utils.FileUtil.CopyFolder(contentDirPath, destinationDirectory);
                        isFinishedContent = true;

                        Utils.LogUtil.Info<NupkgService>($"加载 nupkg 中 非dll: ./content : {sourceFile} ; {contentDirPath}");
                    }
                }
                else
                {
                    // 2.2 ./contentFiles/any/netFolderName
                    // 在 ./content 中没有找到文件, 再尝试 此文件夹
                    if (!isFinishedContent)
                    {
                        string contentFilesDirPath = Path.Combine(destinationDirectory, "contentFiles");
                        DirectoryInfo dir = new DirectoryInfo(contentFilesDirPath);
                        int? childDirLength = dir.GetDirectories()?.Length;
                        if (childDirLength >= 1)
                        {
                            DirectoryInfo anyDir = dir.GetDirectories()[0];
                            DirectoryInfo netFolderDir = anyDir.GetDirectories(netFolderName)?.FirstOrDefault();
                            if (netFolderDir != null)
                            {
                                Utils.FileUtil.CopyFolder(netFolderDir.FullName, destinationDirectory);

                                Utils.LogUtil.Info<NupkgService>($"加载 nupkg 中 非dll: ./contentFiles/any/netFolderName : {sourceFile} ; {netFolderDir.FullName}");
                            }
                        }
                    }
                }





            }
            catch (Exception ex)
            {
                Utils.LogUtil.Error<NupkgService>(ex, ex.Message);
                Utils.LogUtil.Error<NupkgService>(ex.InnerException?.ToString() ?? "");

                throw ex;
            }

            return isDecomparessSuccess;
        }
    }
}


