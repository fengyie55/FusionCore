namespace FusionKernel.Results;

/// <summary>
/// 表示平台底座通用操作结果的最小模型。
/// </summary>
public sealed record OperationResult(
    bool Succeeded,
    string Code,
    string? Message);
