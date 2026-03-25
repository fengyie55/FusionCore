using FusionKernel.Composition;
using FusionKernel.Modules;
using FusionKernel.Results;
using FusionKernel.Runtime;
using FusionKernel.Services;

namespace FusionKernel.Hosting;

/// <summary>
/// 提供宿主边界的最小默认实现。
/// </summary>
public sealed class FusionHost : IFusionHost
{
    public FusionHost(
        IFusionHostContext context,
        HostDescriptor descriptor,
        RuntimeContext runtimeContext,
        IFusionModuleRegistry moduleRegistry,
        IServiceRegistrar serviceRegistrar,
        IServiceResolver serviceResolver)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Descriptor = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
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
    /// 获取宿主描述。
    /// </summary>
    public HostDescriptor Descriptor { get; }

    /// <summary>
    /// 获取运行时上下文。
    /// </summary>
    public RuntimeContext RuntimeContext { get; private set; }

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
    /// 获取当前宿主状态。
    /// </summary>
    public HostState State { get; private set; } = HostState.Constructed;

    /// <summary>
    /// 获取初始化状态。
    /// </summary>
    public HostInitializationState InitializationState { get; private set; } = HostInitializationState.NotInitialized;

    /// <summary>
    /// 获取最小诊断信息。
    /// </summary>
    public HostDiagnosticInfo DiagnosticInfo => CreateDiagnosticInfo();

    /// <summary>
    /// 执行最小初始化。
    /// </summary>
    public void Initialize()
    {
        InitializeHost();
    }

    /// <summary>
    /// 执行宿主初始化。
    /// </summary>
    public HostInitializationResult InitializeHost()
    {
        if (InitializationState == HostInitializationState.Initialized)
        {
            return new HostInitializationResult(
                true,
                Id,
                "HOST_ALREADY_INITIALIZED",
                null,
                InitializationState,
                Array.Empty<ModuleInitializationResult>(),
                DiagnosticInfo);
        }

        State = HostState.Initializing;
        InitializationState = HostInitializationState.Initializing;
        RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Initializing };

        var moduleResults = new List<ModuleInitializationResult>();

        foreach (var module in ModuleRegistry.GetModules())
        {
            ModuleRegistry.TryUpdateState(module.Descriptor.ModuleId, ModuleState.Initializing);

            ModuleInitializationResult result;

            if (module is IModuleLifecycle lifecycle)
            {
                result = lifecycle.InitializeModule(new ModuleInitializationContext(Descriptor, RuntimeContext, ServiceRegistrar));
            }
            else
            {
                module.Initialize();
                module.ConfigureServices(ServiceRegistrar);
                result = new ModuleInitializationResult(true, module.Descriptor.ModuleId, "MODULE_INITIALIZED", null);
            }

            moduleResults.Add(result);
            ModuleRegistry.TryUpdateState(
                module.Descriptor.ModuleId,
                result.Succeeded ? ModuleState.Initialized : ModuleState.Failed);

            if (!result.Succeeded)
            {
                State = HostState.Failed;
                InitializationState = HostInitializationState.Failed;
                RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Failed };

                return new HostInitializationResult(
                    false,
                    Id,
                    "HOST_INITIALIZATION_FAILED",
                    result.Message,
                    InitializationState,
                    moduleResults,
                    DiagnosticInfo);
            }
        }

        State = HostState.Initialized;
        InitializationState = HostInitializationState.Initialized;
        RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Initialized };

        return new HostInitializationResult(
            true,
            Id,
            "HOST_INITIALIZED",
            null,
            InitializationState,
            moduleResults,
            DiagnosticInfo);
    }

    /// <summary>
    /// 执行宿主启动。
    /// </summary>
    public HostStartResult StartHost()
    {
        if (InitializationState == HostInitializationState.NotInitialized)
        {
            var initialization = InitializeHost();
            if (!initialization.Succeeded)
            {
                return new HostStartResult(false, Id, "HOST_START_BLOCKED", initialization.Message, State, null, DiagnosticInfo);
            }
        }

        State = HostState.Starting;
        RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Starting };

        var moduleResults = new List<ModuleStartResult>();

        foreach (var module in ModuleRegistry.GetModules())
        {
            ModuleRegistry.TryUpdateState(module.Descriptor.ModuleId, ModuleState.Starting);

            ModuleStartResult result;

            if (module is IModuleLifecycle lifecycle)
            {
                result = lifecycle.StartModule(new ModuleStartContext(Descriptor, RuntimeContext, ServiceResolver));
            }
            else
            {
                result = new ModuleStartResult(true, module.Descriptor.ModuleId, "MODULE_STARTED", null);
            }

            moduleResults.Add(result);
            ModuleRegistry.TryUpdateState(
                module.Descriptor.ModuleId,
                result.Succeeded ? ModuleState.Started : ModuleState.Failed);

            if (!result.Succeeded)
            {
                State = HostState.Failed;
                RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Failed };
                return new HostStartResult(false, Id, "HOST_START_FAILED", result.Message, State, moduleResults, DiagnosticInfo);
            }
        }

        State = HostState.Started;
        RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Running };
        return new HostStartResult(true, Id, "HOST_STARTED", null, State, moduleResults, DiagnosticInfo);
    }

    /// <summary>
    /// 执行宿主停止。
    /// </summary>
    public HostStopResult StopHost()
    {
        State = HostState.Stopping;
        RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Stopping };

        var moduleResults = new List<ModuleStopResult>();

        foreach (var module in ModuleRegistry.GetModules().Reverse())
        {
            ModuleRegistry.TryUpdateState(module.Descriptor.ModuleId, ModuleState.Stopping);

            ModuleStopResult result;

            if (module is IModuleLifecycle lifecycle)
            {
                result = lifecycle.StopModule(new ModuleStopContext(Descriptor, RuntimeContext));
            }
            else
            {
                result = new ModuleStopResult(true, module.Descriptor.ModuleId, "MODULE_STOPPED", null);
            }

            moduleResults.Add(result);
            ModuleRegistry.TryUpdateState(
                module.Descriptor.ModuleId,
                result.Succeeded ? ModuleState.Stopped : ModuleState.Failed);
        }

        State = HostState.Stopped;
        RuntimeContext = RuntimeContext with { Status = RuntimeStatus.Stopped };
        return new HostStopResult(true, Id, "HOST_STOPPED", null, State, moduleResults);
    }

    /// <summary>
    /// 执行最小异步启动语义。
    /// </summary>
    public ValueTask StartAsync(CancellationToken cancellationToken = default)
    {
        StartHost();
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 执行最小异步停止语义。
    /// </summary>
    public ValueTask StopAsync(CancellationToken cancellationToken = default)
    {
        StopHost();
        return ValueTask.CompletedTask;
    }

    private HostDiagnosticInfo CreateDiagnosticInfo()
    {
        return new HostDiagnosticInfo(
            Descriptor,
            RuntimeContext.Descriptor,
            State,
            InitializationState,
            new[]
            {
                CreateDependency("ConfigurationProvider", RuntimeContext.ConfigurationProvider),
                CreateDependency("ConfigurationSnapshot", RuntimeContext.ConfigurationSnapshot),
                CreateDependency("LoggerWriter", RuntimeContext.LoggerWriter),
                CreateDependency("LoggerContext", RuntimeContext.LoggerContext)
            },
            ModuleRegistry.CreateSnapshot());
    }

    private static HostDependencyDescriptor CreateDependency(string name, object? instance)
    {
        return new HostDependencyDescriptor(name, instance?.GetType().FullName, instance is not null);
    }
}
