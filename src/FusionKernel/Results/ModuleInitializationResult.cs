namespace FusionKernel.Results;

/// <summary>
/// 表示模块初始化结果。
/// </summary>
public sealed record ModuleInitializationResult(
    bool Succeeded,
    string ModuleId,
    string Code,
    string? Message);
