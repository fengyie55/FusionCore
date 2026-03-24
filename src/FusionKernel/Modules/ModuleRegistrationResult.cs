namespace FusionKernel.Modules;

/// <summary>
/// 表示模块注册操作的最小结果。
/// </summary>
public sealed record ModuleRegistrationResult(
    bool Succeeded,
    string ModuleId,
    string? Message);
