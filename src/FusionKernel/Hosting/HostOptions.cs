namespace FusionKernel.Hosting;

/// <summary>
/// 表示宿主最小选项。
/// </summary>
public sealed record HostOptions(
    string HostId,
    string HostName,
    HostRunMode RunMode,
    string RuntimeRoot,
    string? Profile,
    string RuntimeInstanceId);
