namespace FusionKernel.Services;

/// <summary>
/// 定义最小服务注册边界。
/// </summary>
public interface IServiceRegistrar
{
    /// <summary>
    /// 注册服务映射。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <param name="implementationType">实现类型。</param>
    /// <param name="lifetime">生命周期。</param>
    /// <returns>注册结果。</returns>
    ServiceRegistrationResult Register(
        Type serviceType,
        Type implementationType,
        ServiceLifetimeKind lifetime);
}
