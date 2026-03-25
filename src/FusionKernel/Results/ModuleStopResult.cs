namespace FusionKernel.Results;

/// <summary>
/// 表示模块停止结果。
/// </summary>
public sealed record ModuleStopResult(
    bool Succeeded,
    string ModuleId,
    string Code,
    string? Message);
