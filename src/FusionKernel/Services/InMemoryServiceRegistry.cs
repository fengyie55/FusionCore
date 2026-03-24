namespace FusionKernel.Services;

/// <summary>
/// 提供服务注册与解析边界的最小内存实现。
/// </summary>
public sealed class InMemoryServiceRegistry : IServiceRegistrar, IServiceResolver
{
    private readonly Dictionary<Type, ServiceRegistrationEntry> _registrations = new();

    /// <summary>
    /// 注册服务映射。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <param name="implementationType">实现类型。</param>
    /// <param name="lifetime">生命周期。</param>
    /// <returns>注册结果。</returns>
    public ServiceRegistrationResult Register(
        Type serviceType,
        Type implementationType,
        ServiceLifetimeKind lifetime)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(implementationType);

        if (!serviceType.IsAssignableFrom(implementationType) && serviceType != implementationType)
        {
            return new ServiceRegistrationResult(false, serviceType, lifetime, "实现类型与服务类型不兼容。");
        }

        _registrations[serviceType] = new ServiceRegistrationEntry(implementationType, lifetime);
        return new ServiceRegistrationResult(true, serviceType, lifetime, null);
    }

    /// <summary>
    /// 解析已注册服务。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <returns>服务实例。</returns>
    public object? Resolve(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        if (!_registrations.TryGetValue(serviceType, out var entry))
        {
            return null;
        }

        if (entry.Lifetime == ServiceLifetimeKind.Singleton && entry.SingletonInstance is not null)
        {
            return entry.SingletonInstance;
        }

        var instance = Activator.CreateInstance(entry.ImplementationType);

        if (entry.Lifetime == ServiceLifetimeKind.Singleton)
        {
            entry.SingletonInstance = instance;
        }

        return instance;
    }

    private sealed class ServiceRegistrationEntry
    {
        public ServiceRegistrationEntry(Type implementationType, ServiceLifetimeKind lifetime)
        {
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        public Type ImplementationType { get; }

        public ServiceLifetimeKind Lifetime { get; }

        public object? SingletonInstance { get; set; }
    }
}
