﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core;

/// <summary>
/// GPU帮助类
/// </summary>
public static class GpuHelper
{
    /// <summary>
    /// GPU信息
    /// </summary>
    /// <remarks>
    /// 推荐使用，默认有缓存
    /// </remarks>
    public static List<GpuInfo> GpuInfos => Cache.Default.GetOrAdd("GpuInfos", _ => GetGpuInfos(), 60 * 60);

    /// <summary>
    /// 获取GPU信息
    /// </summary>
    /// <returns></returns>
    public static List<GpuInfo> GetGpuInfos()
    {
        List<GpuInfo> gpuInfos = [];

        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                GetLinuxGpuInfo(gpuInfos);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                GetMacOsGpuInfo(gpuInfos);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GetWindowsGpuInfo(gpuInfos);
            }
        }
        catch (Exception ex)
        {
            Log.Error("获取GPU信息出错，" + ex.Message);
            gpuInfos.Add(new GpuInfo
            {
                Name = "Error",
                Description = "Failed to retrieve GPU information"
            });
        }

        return gpuInfos;
    }

    /// <summary>
    /// 获取Windows GPU信息
    /// </summary>
    /// <param name="gpuInfos"></param>
    private static void GetWindowsGpuInfo(List<GpuInfo> gpuInfos)
    {
        var output = ShellHelper.Cmd("wmic", "path win32_VideoController get Name,DriverVersion,AdapterRAM,VideoModeDescription,Status /Value").Trim();
        var lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        GpuInfo? currentGpu = null;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.Contains('='))
            {
                var parts = line.Split('=', 2);
                if (parts.Length != 2)
                {
                    continue;
                }

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                switch (key)
                {
                    case "Name" when !string.IsNullOrEmpty(value):
                        if (currentGpu != null)
                        {
                            gpuInfos.Add(currentGpu);
                        }
                        currentGpu = new GpuInfo
                        {
                            Name = value
                        };
                        break;

                    case "DriverVersion" when currentGpu != null:
                        currentGpu.DriverVersion = value;
                        break;

                    case "AdapterRAM" when currentGpu != null && long.TryParse(value, out var ram):
                        currentGpu.MemoryBytes = ram;
                        break;

                    case "VideoModeDescription" when currentGpu != null:
                        currentGpu.VideoModeDescription = value;
                        break;

                    case "Status" when currentGpu != null:
                        currentGpu.Status = value;
                        break;
                }
            }
        }

        if (currentGpu != null)
        {
            gpuInfos.Add(currentGpu);
        }
    }

    /// <summary>
    /// 获取Linux GPU信息
    /// </summary>
    /// <param name="gpuInfos"></param>
    /// <exception cref="Exception"></exception>
    private static void GetLinuxGpuInfo(List<GpuInfo> gpuInfos)
    {
        try
        {
            // 尝试使用lspci获取GPU信息
            var output = ShellHelper.Bash("lspci | grep -i vga").Trim();
            var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var gpuInfo = new GpuInfo();

                // 解析lspci输出
                var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    gpuInfo.Name = parts[1].Trim();
                    gpuInfo.BusInfo = parts[0].Trim();
                }

                // 尝试获取更多详细信息
                try
                {
                    var detailOutput = ShellHelper.Bash($"lspci -v -s {gpuInfo.BusInfo}").Trim();
                    var detailLines = detailOutput.Split('\n');

                    foreach (var detailLine in detailLines)
                    {
                        if (detailLine.Contains("Kernel driver in use:"))
                        {
                            gpuInfo.DriverVersion = detailLine.Split(':')[1].Trim();
                        }
                    }
                }
                catch
                {
                    // 获取详细信息失败，继续处理其他GPU
                }

                gpuInfos.Add(gpuInfo);
            }

            // 如果支持nvidia-smi，获取NVIDIA GPU的更多信息
            try
            {
                var nvidiaOutput = ShellHelper.Bash("nvidia-smi --query-gpu=name,driver_version,memory.total,temperature.gpu --format=csv,noheader,nounits").Trim();
                var nvidiaLines = nvidiaOutput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < nvidiaLines.Length && i < gpuInfos.Count; i++)
                {
                    var parts = nvidiaLines[i].Split(',');
                    if (parts.Length >= 4)
                    {
                        var gpuInfo = gpuInfos[i];
                        gpuInfo.Name = parts[0].Trim();
                        gpuInfo.DriverVersion = parts[1].Trim();

                        if (long.TryParse(parts[2].Trim(), out var memory))
                        {
                            gpuInfo.MemoryBytes = memory * 1024 * 1024; // 转换为字节
                        }

                        if (double.TryParse(parts[3].Trim(), out var temp))
                        {
                            gpuInfo.Temperature = temp;
                        }
                    }
                }
            }
            catch
            {
                // nvidia-smi不可用或执行失败
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"获取Linux GPU信息失败: {ex.Message}");
        }
    }

    /// <summary>
    /// 获取macOS GPU信息
    /// </summary>
    /// <param name="gpuInfos"></param>
    /// <exception cref="Exception"></exception>
    private static void GetMacOsGpuInfo(List<GpuInfo> gpuInfos)
    {
        try
        {
            var output = ShellHelper.Bash("system_profiler SPDisplaysDataType").Trim();
            var lines = output.Split('\n');

            GpuInfo? currentGpu = null;

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine.EndsWith(':') && !trimmedLine.Contains(' '))
                {
                    // 这可能是GPU名称
                    if (currentGpu != null)
                    {
                        gpuInfos.Add(currentGpu);
                    }

                    currentGpu = new GpuInfo
                    {
                        Name = trimmedLine.TrimEnd(':')
                    };
                }
                else if (currentGpu != null && trimmedLine.Contains(':'))
                {
                    var parts = trimmedLine.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();

                        switch (key.ToLower())
                        {
                            case "vram (total)" when value.Contains("MB"):
                                var mbValue = value.Replace("MB", "").Trim();
                                if (long.TryParse(mbValue, out var memory))
                                {
                                    currentGpu.MemoryBytes = memory * 1024 * 1024;
                                }
                                break;

                            case "vendor":
                                currentGpu.Vendor = value;
                                break;

                            case "device id":
                                currentGpu.DeviceId = value;
                                break;
                        }
                    }
                }
            }

            if (currentGpu != null)
            {
                gpuInfos.Add(currentGpu);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"获取macOS GPU信息失败: {ex.Message}");
        }
    }
}

/// <summary>
/// GPU信息
/// </summary>
public record GpuInfo
{
    /// <summary>
    /// GPU名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 厂商
    /// </summary>
    public string Vendor { get; set; } = string.Empty;

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; } = string.Empty;

    /// <summary>
    /// 总线信息
    /// </summary>
    public string BusInfo { get; set; } = string.Empty;

    /// <summary>
    /// 驱动版本
    /// </summary>
    public string DriverVersion { get; set; } = string.Empty;

    /// <summary>
    /// 显存大小（字节）
    /// </summary>
    public long MemoryBytes { get; set; }

    /// <summary>
    /// GPU温度（°C）
    /// </summary>
    public double? Temperature { get; set; }

    /// <summary>
    /// 视频模式描述
    /// </summary>
    public string VideoModeDescription { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// GPU使用率（%）
    /// </summary>
    public double? UtilizationPercentage { get; set; }

    /// <summary>
    /// 显存使用率（%）
    /// </summary>
    public double? MemoryUtilizationPercentage { get; set; }
}