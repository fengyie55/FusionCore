namespace FusionKernel.Hosting;

/// <summary>
/// 表示宿主的最小描述信息。
/// </summary>
public sealed record HostDescriptor(
    string HostId,
    string HostName,
    string RuntimeInstanceId,
    string RuntimeRoot,
    HostRunMode RunMode,
    string? Profile);
