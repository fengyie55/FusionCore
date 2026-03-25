using FusionKernel.Hosting;

namespace FusionKernel.Composition;

/// <summary>
/// 表示宿主组合最小选项。
/// </summary>
public sealed record HostCompositionOptions(
    string HostId,
    string HostName,
    string RuntimeInstanceId,
    string RuntimeRoot,
    HostRunMode RunMode,
    string? Profile);
