namespace FusionKernel.Services;

/// <summary>
/// 表示服务注册操作的最小结果。
/// </summary>
public sealed record ServiceRegistrationResult(
    bool Succeeded,
    Type ServiceType,
    ServiceLifetimeKind Lifetime,
    string? Message);
