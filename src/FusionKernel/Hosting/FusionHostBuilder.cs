using FusionKernel.Runtime;
using FusionKernel.Modules;
using FusionKernel.Services;

namespace FusionKernel.Hosting;

/// <summary>
/// 提供宿主构建器的最小默认实现。
/// </summary>
public sealed class FusionHostBuilder : IFusionHostBuilder
{
    private IFusionHostContext? _hostContext;
    private RuntimeContext? _runtimeContext;
    private IFusionModuleRegistry? _moduleRegistry;
    private IServiceRegistrar? _serviceRegistrar;
    private IServiceResolver? _serviceResolver;

    /// <summary>
    /// 配置运行上下文。
    /// </summary>
    public IFusionHostBuilder UseRuntimeContext(RuntimeContext runtimeContext)
    {
        _runtimeContext = runtimeContext ?? throw new ArgumentNullException(nameof(runtimeContext));
        return this;
    }

    /// <summary>
    /// 配置宿主上下文。
    /// </summary>
    public IFusionHostBuilder UseHostContext(IFusionHostContext hostContext)
    {
        _hostContext = hostContext ?? throw new ArgumentNullException(nameof(hostContext));
        return this;
    }

    /// <summary>
    /// 配置模块注册表。
    /// </summary>
    public FusionHostBuilder UseModuleRegistry(IFusionModuleRegistry moduleRegistry)
    {
        _moduleRegistry = moduleRegistry ?? throw new ArgumentNullException(nameof(moduleRegistry));
        return this;
    }

    /// <summary>
    /// 配置服务注册器。
    /// </summary>
    public FusionHostBuilder UseServiceRegistrar(IServiceRegistrar serviceRegistrar)
    {
        _serviceRegistrar = serviceRegistrar ?? throw new ArgumentNullException(nameof(serviceRegistrar));
        return this;
    }

    /// <summary>
    /// 配置服务解析器。
    /// </summary>
    public FusionHostBuilder UseServiceResolver(IServiceResolver serviceResolver)
    {
        _serviceResolver = serviceResolver ?? throw new ArgumentNullException(nameof(serviceResolver));
        return this;
    }

    /// <summary>
    /// 构建最小宿主实例。
    /// </summary>
    public IFusionHost Build()
    {
        var runtimeContext = _runtimeContext ?? CreateDefaultRuntimeContext();
        var serviceRegistry = CreateOrReuseServiceRegistry();
        var hostContext = _hostContext ?? new FusionHostContext(
            "FusionHost",
            "Fusion Host",
            runtimeContext.RunMode,
            runtimeContext.RuntimeRoot,
            runtimeContext);

        var descriptor = new HostDescriptor(
            hostContext.HostId,
            hostContext.HostName,
            runtimeContext.InstanceId.Value,
            runtimeContext.RuntimeRoot,
            hostContext.RunMode,
            runtimeContext.Profile);

        return new FusionHost(
            hostContext,
            descriptor,
            runtimeContext,
            _moduleRegistry ?? new InMemoryFusionModuleRegistry(),
            _serviceRegistrar ?? serviceRegistry,
            _serviceResolver ?? serviceRegistry);
    }

    private static RuntimeContext CreateDefaultRuntimeContext()
    {
        return new RuntimeContext(
            new RuntimeInstanceId("FusionRuntime"),
            AppContext.BaseDirectory,
            HostRunMode.Production,
            "Production");
    }

    private InMemoryServiceRegistry CreateOrReuseServiceRegistry()
    {
        if (_serviceRegistrar is InMemoryServiceRegistry registrarRegistry && _serviceResolver is null)
        {
            return registrarRegistry;
        }

        if (_serviceResolver is InMemoryServiceRegistry resolverRegistry && _serviceRegistrar is null)
        {
            return resolverRegistry;
        }

        if (_serviceRegistrar is InMemoryServiceRegistry sharedRegistry &&
            ReferenceEquals(_serviceRegistrar, _serviceResolver))
        {
            return sharedRegistry;
        }

        return new InMemoryServiceRegistry();
    }
}
