namespace FusionKernel.Results;

/// <summary>
/// 表示模块激活操作的最小结果。
/// </summary>
public sealed record ModuleActivationResult(
    bool Succeeded,
    string ModuleId,
    string Code,
    string? Message);
