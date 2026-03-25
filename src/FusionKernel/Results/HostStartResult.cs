using FusionKernel.Hosting;

namespace FusionKernel.Results;

/// <summary>
/// 表示宿主启动操作的最小结果。
/// </summary>
public sealed record HostStartResult(
    bool Succeeded,
    string HostId,
    string Code,
    string? Message,
    HostState State = HostState.Constructed,
    IReadOnlyList<ModuleStartResult>? ModuleResults = null,
    HostDiagnosticInfo? DiagnosticInfo = null);
