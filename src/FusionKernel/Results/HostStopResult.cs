using FusionKernel.Hosting;

namespace FusionKernel.Results;

/// <summary>
/// 表示宿主停止结果。
/// </summary>
public sealed record HostStopResult(
    bool Succeeded,
    string HostId,
    string Code,
    string? Message,
    HostState State = HostState.Stopped,
    IReadOnlyList<ModuleStopResult>? ModuleResults = null);
