using FusionKernel.Hosting;

namespace FusionKernel.Results;

/// <summary>
/// 表示宿主初始化操作的最小结果。
/// </summary>
public sealed record HostInitializationResult(
    bool Succeeded,
    string HostId,
    string Code,
    string? Message,
    HostInitializationState InitializationState = HostInitializationState.NotInitialized,
    IReadOnlyList<ModuleInitializationResult>? ModuleResults = null,
    HostDiagnosticInfo? DiagnosticInfo = null);
