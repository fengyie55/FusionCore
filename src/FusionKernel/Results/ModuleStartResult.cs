namespace FusionKernel.Results;

/// <summary>
/// 表示模块启动结果。
/// </summary>
public sealed record ModuleStartResult(
    bool Succeeded,
    string ModuleId,
    string Code,
    string? Message);
