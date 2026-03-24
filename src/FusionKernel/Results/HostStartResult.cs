namespace FusionKernel.Results;

/// <summary>
/// 表示宿主启动操作的最小结果。
/// </summary>
public sealed record HostStartResult(
    bool Succeeded,
    string HostId,
    string Code,
    string? Message);
