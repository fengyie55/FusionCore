using FusionKernel.Context;
using FusionKernel.Lifecycle;
using FusionKernel.Modules;
using FusionKernel.Results;
using FusionKernel.Services;

namespace FusionKernel.Hosting;

/// <summary>
/// 提供宿主边界的最小默认实现。
/// </summary>
public sealed class FusionHost : IFusionHost
{
    /// <summary>
    /// 创建宿主实例。
    /// </summary>
    /// <param name="context">宿主上下文。</param>
    /// <param name="runtimeContext">运行上下文。</param>
    /// <param name="moduleRegistry">模块注册表。</param>
    /// <param name="serviceRegistrar">服务注册器。</param>
    /// <param name="serviceResolver">服务解析器。</param>
    public FusionHost(
        IFusionHostContext context,
        RuntimeContext runtimeContext,
        IFusionModuleRegistry moduleRegistry,
        IServiceRegistrar serviceRegistrar,
        IServiceResolver serviceResolver)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        RuntimeContext = runtimeContext ?? throw new ArgumentNullException(nameof(runtimeContext));
        ModuleRegistry = moduleRegistry ?? throw new ArgumentNullException(nameof(moduleRegistry));
        ServiceRegistrar = serviceRegistrar ?? throw new ArgumentNullException(nameof(serviceRegistrar));
        ServiceResolver = serviceResolver ?? throw new ArgumentNullException(nameof(serviceResolver));
    }

    /// <summary>
    /// 获取宿主标识。
    /// </summary>
    public string Id => Context.HostId;

    /// <summary>
    /// 获取宿主名称。
    /// </summary>
    public string Name => Context.HostName;

    /// <summary>
    /// 获取宿主上下文。
    /// </summary>
    public IFusionHostContext Context { get; }

    /// <summary>
    /// 获取运行上下文。
    /// </summary>
    public RuntimeContext RuntimeContext { get; }

    /// <summary>
    /// 获取模块注册表。
    /// </summary>
    public IFusionModuleRegistry ModuleRegistry { get; }

    /// <summary>
    /// 获取服务注册器。
    /// </summary>
    public IServiceRegistrar ServiceRegistrar { get; }

    /// <summary>
    /// 获取服务解析器。
    /// </summary>
    public IServiceResolver ServiceResolver { get; }

    /// <summary>
    /// 获取当前生命周期状态。
    /// </summary>
    public LifecycleState State { get; private set; } = LifecycleState.Created;

    /// <summary>
    /// 执行最小初始化。
    /// </summary>
    public void Initialize()
    {
        InitializeHost();
    }

    /// <summary>
    /// 初始化宿主与已注册模块。
    /// </summary>
    /// <returns>初始化结果。</returns>
    public HostInitializationResult InitializeHost()
    {
        foreach (var descriptor in ModuleRegistry.GetRegisteredModules())
        {
            if (ModuleRegistry.TryGetModule(descriptor.ModuleId, out var module) && module is not null)
            {
                module.Initialize();
                module.ConfigureServices(ServiceRegistrar);
            }
        }

        State = LifecycleState.Initialized;
        return new HostInitializationResult(true, Id, "HOST_INITIALIZED", null);
    }

    /// <summary>
    /// 执行最小启动语义。
    /// </summary>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>异步任务。</returns>
    public ValueTask StartAsync(CancellationToken cancellationToken = default)
    {
        if (State == LifecycleState.Created)
        {
            InitializeHost();
        }

        State = LifecycleState.Starting;
        State = LifecycleState.Started;
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 执行最小停止语义。
    /// </summary>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>异步任务。</returns>
    public ValueTask StopAsync(CancellationToken cancellationToken = default)
    {
        State = LifecycleState.Stopping;
        State = LifecycleState.Stopped;
        return ValueTask.CompletedTask;
    }
}
