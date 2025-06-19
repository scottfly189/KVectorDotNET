﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.IO.Compression;

namespace Admin.NET.Core;

/// <summary>
/// 压缩帮助类
/// </summary>
public static class CompressHelper
{
    /// <summary>
    /// 解压文件
    /// </summary>
    /// <param name="archivePath">压缩文件路径</param>
    /// <param name="extractPath">解压目标路径</param>
    /// <param name="format">压缩格式</param>
    /// <exception cref="FileNotFoundException">文件不存在时抛出</exception>
    /// <exception cref="DirectoryNotFoundException">目录不存在时抛出</exception>
    public static void Extract(string archivePath, string extractPath, CompressionFormat format = CompressionFormat.Zip)
    {
        if (!File.Exists(archivePath))
        {
            throw new FileNotFoundException("没有找到文件。", archivePath);
        }

        if (!Directory.Exists(extractPath))
        {
            _ = Directory.CreateDirectory(extractPath);
        }

        switch (format)
        {
            case CompressionFormat.Zip:
                ZipFile.ExtractToDirectory(archivePath, extractPath);
                break;

            case CompressionFormat.GZip:
                ExtractGZip(archivePath, extractPath);
                break;

            case CompressionFormat.Deflate:
                ExtractDeflate(archivePath, extractPath);
                break;

            default:
                throw new ArgumentException("不支持的压缩格式。", nameof(format));
        }
    }

    /// <summary>
    /// 压缩文件或目录
    /// </summary>
    /// <param name="sourcePath">源文件或目录路径</param>
    /// <param name="archivePath">压缩文件保存路径</param>
    /// <param name="format">压缩格式</param>
    /// <param name="level">压缩级别</param>
    /// <exception cref="FileNotFoundException">文件不存在时抛出</exception>
    /// <exception cref="DirectoryNotFoundException">目录不存在时抛出</exception>
    public static void Compress(string sourcePath, string archivePath, CompressionFormat format = CompressionFormat.Zip, CompressionLevel level = CompressionLevel.Optimal)
    {
        if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
        {
            throw new FileNotFoundException("源文件或目录不存在。", sourcePath);
        }

        switch (format)
        {
            case CompressionFormat.Zip:
                if (Directory.Exists(sourcePath))
                {
                    ZipFile.CreateFromDirectory(sourcePath, archivePath, level, false);
                }
                else
                {
                    CompressFileToZip(sourcePath, archivePath, level);
                }
                break;

            case CompressionFormat.GZip:
                CompressToGZip(sourcePath, archivePath, level);
                break;

            case CompressionFormat.Deflate:
                CompressToDeflate(sourcePath, archivePath, level);
                break;

            default:
                throw new ArgumentException("不支持的压缩格式。", nameof(format));
        }
    }

    /// <summary>
    /// 压缩单个文件到ZIP
    /// </summary>
    private static void CompressFileToZip(string sourceFile, string archivePath, CompressionLevel level)
    {
        using var archive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
        archive.CreateEntryFromFile(sourceFile, Path.GetFileName(sourceFile), level);
    }

    /// <summary>
    /// 压缩到GZIP
    /// </summary>
    private static void CompressToGZip(string sourcePath, string archivePath, CompressionLevel level)
    {
        using var sourceStream = File.OpenRead(sourcePath);
        using var destinationStream = File.Create(archivePath);
        using var gzipStream = new GZipStream(destinationStream, level);
        sourceStream.CopyTo(gzipStream);
    }

    /// <summary>
    /// 从GZIP解压
    /// </summary>
    private static void ExtractGZip(string archivePath, string extractPath)
    {
        using var sourceStream = File.OpenRead(archivePath);
        using var gzipStream = new GZipStream(sourceStream, CompressionMode.Decompress);
        using var destinationStream = File.Create(Path.Combine(extractPath, Path.GetFileNameWithoutExtension(archivePath)));
        gzipStream.CopyTo(destinationStream);
    }

    /// <summary>
    /// 压缩到DEFLATE
    /// </summary>
    private static void CompressToDeflate(string sourcePath, string archivePath, CompressionLevel level)
    {
        using var sourceStream = File.OpenRead(sourcePath);
        using var destinationStream = File.Create(archivePath);
        using var deflateStream = new DeflateStream(destinationStream, level);
        sourceStream.CopyTo(deflateStream);
    }

    /// <summary>
    /// 从DEFLATE解压
    /// </summary>
    private static void ExtractDeflate(string archivePath, string extractPath)
    {
        using var sourceStream = File.OpenRead(archivePath);
        using var deflateStream = new DeflateStream(sourceStream, CompressionMode.Decompress);
        using var destinationStream = File.Create(Path.Combine(extractPath, Path.GetFileNameWithoutExtension(archivePath)));
        deflateStream.CopyTo(destinationStream);
    }

    /// <summary>
    /// 压缩格式
    /// </summary>
    public enum CompressionFormat
    {
        /// <summary>
        /// ZIP格式
        /// </summary>
        Zip,

        /// <summary>
        /// GZIP格式
        /// </summary>
        GZip,

        /// <summary>
        /// DEFLATE格式
        /// </summary>
        Deflate
    }
}