﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Admin.NET.Core;

/// <summary>
/// 网卡信息帮助类
/// </summary>
public static class NetworkHelper
{
    /// <summary>
    /// 网卡信息
    /// </summary>
    /// <remarks>
    /// 推荐使用，默认有缓存
    /// </remarks>
    public static List<NetworkInfo> NetworkInfos => Cache.Default.GetOrAdd("NetworkInfos", _ => GetNetworkInfos(), 5 * 60);

    /// <summary>
    /// 获取网卡信息
    /// </summary>
    /// <returns></returns>
    public static List<NetworkInfo> GetNetworkInfos()
    {
        List<NetworkInfo> networkInfos = [];

        try
        {
            // 获取所有网络接口
            var interfaces = NetworkInterface.GetAllNetworkInterfaces().ToList();

            foreach (var ni in interfaces)
            {
                var networkInfo = new NetworkInfo
                {
                    Name = ni.Name,
                    Description = ni.Description,
                    Type = ni.NetworkInterfaceType.ToString(),
                    OperationalStatus = ni.OperationalStatus.ToString(),
                    Speed = ni.Speed > 0 ? ni.Speed.ToString("#,##0") + " bps" : "Unknown",
                    PhysicalAddress = BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes()),
                    SupportsMulticast = ni.SupportsMulticast,
                    IsReceiveOnly = ni.IsReceiveOnly
                };

                try
                {
                    var properties = ni.GetIPProperties();
                    networkInfo.DnsAddresses = [.. properties.DnsAddresses.Select(ip => ip.ToString())];
                    networkInfo.GatewayAddresses = [.. properties.GatewayAddresses.Select(gw => gw.Address.ToString())];

                    // DHCP服务器地址在macOS上不受支持
                    if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        networkInfo.DhcpServerAddresses = [.. properties.DhcpServerAddresses.Select(ip => ip.ToString())];
                    }

                    // IPv4 地址信息
                    var unicastAddresses = properties.UnicastAddresses.ToList();
                    networkInfo.IPv4Addresses = [.. unicastAddresses
                        .Where(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)
                        .Select(addr => new IpAddressInfo
                        {
                            Address = addr.Address.ToString(),
                            SubnetMask = addr.IPv4Mask?.ToString() ?? "",
                            PrefixLength = addr.PrefixLength
                        })];

                    networkInfo.IPv6Addresses = [.. unicastAddresses
                        .Where(addr => addr.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        .Select(addr => new IpAddressInfo
                        {
                            Address = addr.Address.ToString(),
                            PrefixLength = addr.PrefixLength
                        })];

                    // 获取网络统计信息
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        var stats = ni.GetIPv4Statistics();
                        networkInfo.Statistics = new NetworkInterfaceStatistics
                        {
                            BytesReceived = stats.BytesReceived,
                            BytesSent = stats.BytesSent,
                            PacketsReceived = stats.UnicastPacketsReceived + stats.NonUnicastPacketsReceived,
                            PacketsSent = stats.UnicastPacketsSent + stats.NonUnicastPacketsSent,
                            IncomingPacketsDiscarded = stats.IncomingPacketsDiscarded,
                            IncomingPacketsWithErrors = stats.IncomingPacketsWithErrors,
                            OutgoingPacketsWithErrors = stats.OutgoingPacketsWithErrors
                        };

                        // OutgoingPacketsDiscarded在macOS上不受支持
                        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        {
                            networkInfo.Statistics.OutgoingPacketsDiscarded = stats.OutgoingPacketsDiscarded;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 如果获取IP属性失败，记录错误信息
                    Log.Error($"获取网卡 {ni.Name} 的IP属性出错，{ex.Message}");
                }

                if (networkInfo.DhcpServerAddresses.Count == 0 && networkInfo.DnsAddresses.Count == 0 &&
                    networkInfo.GatewayAddresses.Count == 0 && networkInfo.IPv4Addresses.Count == 0 &&
                    networkInfo.IPv6Addresses.Count == 0)
                {
                    continue;
                }

                networkInfos.Add(networkInfo);
            }
        }
        catch (Exception ex)
        {
            // 如果完全失败，返回包含错误信息的空列表
            Log.Error("获取网卡信息出错，" + ex.Message);
            networkInfos.Add(new NetworkInfo
            {
                Name = "Error",
                Description = "Failed to retrieve network information"
            });
        }

        return networkInfos;
    }
}

/// <summary>
/// 网卡信息
/// </summary>
public record NetworkInfo
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 操作状态
    /// </summary>
    public string OperationalStatus { get; set; } = string.Empty;

    /// <summary>
    /// 速度
    /// </summary>
    public string Speed { get; set; } = string.Empty;

    /// <summary>
    /// 物理地址(mac 地址)
    /// </summary>
    public string PhysicalAddress { get; set; } = string.Empty;

    /// <summary>
    /// 是否支持多播
    /// </summary>
    public bool SupportsMulticast { get; set; }

    /// <summary>
    /// 是否只接收
    /// </summary>
    public bool IsReceiveOnly { get; set; }

    /// <summary>
    /// DNS 地址
    /// </summary>
    public List<string> DnsAddresses { get; set; } = [];

    /// <summary>
    /// 网关地址
    /// </summary>
    public List<string> GatewayAddresses { get; set; } = [];

    /// <summary>
    /// DHCP服务器地址
    /// </summary>
    public List<string> DhcpServerAddresses { get; set; } = [];

    /// <summary>
    /// IPv4 地址详细信息
    /// </summary>
    public List<IpAddressInfo> IPv4Addresses { get; set; } = [];

    /// <summary>
    /// IPv6 地址详细信息
    /// </summary>
    public List<IpAddressInfo> IPv6Addresses { get; set; } = [];

    /// <summary>
    /// 网络接口统计信息
    /// </summary>
    public NetworkInterfaceStatistics? Statistics { get; set; }
}

/// <summary>
/// IP地址信息
/// </summary>
public record IpAddressInfo
{
    /// <summary>
    /// IP地址
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// 子网掩码
    /// </summary>
    public string SubnetMask { get; set; } = string.Empty;

    /// <summary>
    /// 前缀长度
    /// </summary>
    public int PrefixLength { get; set; }
}

/// <summary>
/// 网络接口统计信息
/// </summary>
public record NetworkInterfaceStatistics
{
    /// <summary>
    /// 接收字节数
    /// </summary>
    public long BytesReceived { get; set; }

    /// <summary>
    /// 发送字节数
    /// </summary>
    public long BytesSent { get; set; }

    /// <summary>
    /// 接收数据包数
    /// </summary>
    public long PacketsReceived { get; set; }

    /// <summary>
    /// 发送数据包数
    /// </summary>
    public long PacketsSent { get; set; }

    /// <summary>
    /// 丢弃的传入数据包数
    /// </summary>
    public long IncomingPacketsDiscarded { get; set; }

    /// <summary>
    /// 丢弃的传出数据包数
    /// </summary>
    public long OutgoingPacketsDiscarded { get; set; }

    /// <summary>
    /// 传入错误数据包数
    /// </summary>
    public long IncomingPacketsWithErrors { get; set; }

    /// <summary>
    /// 传出错误数据包数
    /// </summary>
    public long OutgoingPacketsWithErrors { get; set; }
}

/// <summary>
/// 网络统计信息
/// </summary>
public record NetworkStatistics
{
    /// <summary>
    /// 总接收字节数
    /// </summary>
    public long TotalBytesReceived { get; set; }

    /// <summary>
    /// 总发送字节数
    /// </summary>
    public long TotalBytesSent { get; set; }

    /// <summary>
    /// 总接收数据包数
    /// </summary>
    public long TotalPacketsReceived { get; set; }

    /// <summary>
    /// 总发送数据包数
    /// </summary>
    public long TotalPacketsSent { get; set; }
}