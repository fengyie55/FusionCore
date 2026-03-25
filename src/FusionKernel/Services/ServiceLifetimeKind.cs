namespace FusionKernel.Services;

/// <summary>
/// 表示服务生命周期的最小分类。
/// </summary>
public enum ServiceLifetimeKind
{
    Singleton = 0,
    Scoped = 1,
    Transient = 2,
}
