﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core;

/// <summary>
/// 操作系统平台帮助类
/// </summary>
public static class OsPlatformHelper
{
    /// <summary>
    /// 是否为 Unix 系统
    /// </summary>
    public static bool IsUnixSystem { get; } = GetIsUnixSystem();

    /// <summary>
    /// 操作系统平台类型
    /// </summary>
    public static OSPlatform PlatformType { get; } = GetPlatformType();

    /// <summary>
    /// 是否 Windows 平台
    /// </summary>
    public static bool IsWindows => PlatformType == OSPlatform.Windows;

    /// <summary>
    /// 是否 Linux 平台
    /// </summary>
    public static bool IsLinux => PlatformType == OSPlatform.Linux;

    /// <summary>
    /// 是否 macOS 平台
    /// </summary>
    public static bool IsMacOs => PlatformType == OSPlatform.OSX;

    /// <summary>
    /// 是否 FreeBSD 平台
    /// </summary>
    public static bool IsFreeBsd => PlatformType == OSPlatform.FreeBSD;

    /// <summary>
    /// 处理器信息
    /// </summary>
    /// <remarks>
    /// 推荐使用，默认有缓存
    /// </remarks>
    public static RuntimeInfo RuntimeInfos => Cache.Default.GetOrAdd("RuntimeInfos", _ => GetRuntimeInfo(), 60);

    /// <summary>
    /// 收集运行时信息
    /// </summary>
    public static RuntimeInfo GetRuntimeInfo()
    {
        try
        {
            var currentProcess = Process.GetCurrentProcess();
            var runtimeInfo = new RuntimeInfo
            {
                OsName = GetOperatingSystemName(),
                OsDescription = RuntimeInformation.OSDescription,
                OsVersion = Environment.OSVersion.Version.ToString(),
                OsArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                RuntimeVersion = Environment.Version.ToString(),
                DatabaseType = App.GetOptions<DbConnectionOptions>().ConnectionConfigs[0].DbType.ToString(),
                Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
                Is64BitProcess = Environment.Is64BitProcess,
                IsInteractive = Environment.UserInteractive,
                ProcessorCount = Environment.ProcessorCount,
                SystemDirectory = Environment.SystemDirectory,
                CurrentDirectory = Environment.CurrentDirectory,
                MachineName = Environment.MachineName,
                UserName = Environment.UserName,
                UserDomainName = Environment.UserDomainName,
                WorkingSet = Environment.WorkingSet,
                SystemStartTime = DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount64),
                SystemUptime = TimeSpan.FromMilliseconds(Environment.TickCount64),
                ProcessStartTime = currentProcess.StartTime,
                ProcessUptime = DateTime.Now - currentProcess.StartTime,
                ProcessId = currentProcess.Id,
                ProcessName = currentProcess.ProcessName,
                ClrVersion = Environment.Version.ToString(),
                EnvironmentVariableCount = Environment.GetEnvironmentVariables().Count,
                CommandLineArgs = Environment.GetCommandLineArgs()
            };

            return runtimeInfo;
        }
        catch (Exception ex)
        {
            // 异常时返回基本信息
            return new RuntimeInfo
            {
                OsName = "Unknown",
                OsDescription = $"Error collecting info: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// 获取环境变量
    /// </summary>
    /// <param name="variableName">变量名</param>
    /// <param name="target">环境变量目标</param>
    /// <returns>环境变量值</returns>
    public static string? GetEnvironmentVariable(string variableName, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
    {
        return Environment.GetEnvironmentVariable(variableName, target);
    }

    /// <summary>
    /// 获取所有环境变量
    /// </summary>
    /// <param name="target">环境变量目标</param>
    /// <returns>环境变量字典</returns>
    public static Dictionary<string, string?> GetAllEnvironmentVariables(EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
    {
        var variables = Environment.GetEnvironmentVariables(target);
        return variables.Cast<DictionaryEntry>()
            .ToDictionary(entry => entry.Key.ToString()!, entry => entry.Value?.ToString());
    }

    /// <summary>
    /// 检查特定的操作系统版本
    /// </summary>
    /// <param name="minimumVersion">最小版本要求</param>
    /// <returns>是否满足版本要求</returns>
    public static bool CheckOsVersion(Version minimumVersion)
    {
        try
        {
            return Environment.OSVersion.Version >= minimumVersion;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 获取.NET运行时位置
    /// </summary>
    /// <returns>运行时路径</returns>
    public static string GetRuntimeLocation()
    {
        try
        {
            return Path.GetDirectoryName(typeof(object).Assembly.Location) ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 判断当前操作系统是否为 Unix 系统
    /// </summary>
    /// <returns></returns>
    public static bool GetIsUnixSystem()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                                RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                                RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);
    }

    /// <summary>
    /// 获取操作系统平台类型
    /// </summary>
    private static OSPlatform GetPlatformType()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? OSPlatform.Windows
            : RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            ? OSPlatform.Linux
            : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
            ? OSPlatform.OSX
            : RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD)
            ? OSPlatform.FreeBSD
            : OSPlatform.Create("Unknown");
    }

    /// <summary>
    /// 获取操作系统名称
    /// </summary>
    private static string GetOperatingSystemName()
    {
        var platformType = GetPlatformType();
        return platformType.ToString() switch
        {
            "Windows" => "Windows",
            "Linux" => "Linux",
            "OSX" => "MacOS",
            "FreeBSD" => "FreeBSD",
            _ => "Unknown"
        };
    }
}

/// <summary>
/// 系统运行时信息
/// </summary>
public record RuntimeInfo
{
    /// <summary>
    /// 操作系统名称
    /// </summary>
    public string OsName { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统描述
    /// </summary>
    public string OsDescription { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统版本
    /// </summary>
    public string OsVersion { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统架构
    /// </summary>
    public string OsArchitecture { get; set; } = string.Empty;

    /// <summary>
    /// 进程架构
    /// </summary>
    public string ProcessArchitecture { get; set; } = string.Empty;

    /// <summary>
    /// 运行时框架描述
    /// </summary>
    public string FrameworkDescription { get; set; } = string.Empty;

    /// <summary>
    /// 运行时版本
    /// </summary>
    public string RuntimeVersion { get; set; } = string.Empty;

    /// <summary>
    /// 数据库类型
    /// </summary>
    public string DatabaseType { get; set; } = string.Empty;

    /// <summary>
    /// 是否64位操作系统
    /// </summary>
    public bool Is64BitOperatingSystem { get; set; }

    /// <summary>
    /// 是否64位进程
    /// </summary>
    public bool Is64BitProcess { get; set; }

    /// <summary>
    /// 是否交互模式
    /// </summary>
    public bool IsInteractive { get; set; }

    /// <summary>
    /// 交互模式描述
    /// </summary>
    public string InteractiveMode => IsInteractive ? "交互运行" : "非交互运行";

    /// <summary>
    /// 处理器数量
    /// </summary>
    public int ProcessorCount { get; set; }

    /// <summary>
    /// 系统目录
    /// </summary>
    public string SystemDirectory { get; set; } = string.Empty;

    /// <summary>
    /// 当前目录
    /// </summary>
    public string CurrentDirectory { get; set; } = string.Empty;

    /// <summary>
    /// 机器名称
    /// </summary>
    public string MachineName { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 用户域名
    /// </summary>
    public string UserDomainName { get; set; } = string.Empty;

    /// <summary>
    /// 工作集大小（字节）
    /// </summary>
    public long WorkingSet { get; set; }

    /// <summary>
    /// 系统启动时间
    /// </summary>
    public DateTime SystemStartTime { get; set; }

    /// <summary>
    /// 系统运行时间
    /// </summary>
    public TimeSpan SystemUptime { get; set; }

    /// <summary>
    /// 进程启动时间
    /// </summary>
    public DateTime ProcessStartTime { get; set; }

    /// <summary>
    /// 进程运行时间
    /// </summary>
    public TimeSpan ProcessUptime { get; set; }

    /// <summary>
    /// 进程ID
    /// </summary>
    public int ProcessId { get; set; }

    /// <summary>
    /// 进程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// CLR版本
    /// </summary>
    public string ClrVersion { get; set; } = string.Empty;

    /// <summary>
    /// 环境变量数量
    /// </summary>
    public int EnvironmentVariableCount { get; set; }

    /// <summary>
    /// 命令行参数
    /// </summary>
    public string[] CommandLineArgs { get; set; } = [];
}