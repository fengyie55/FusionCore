using FusionKernel.Hosting;
using FusionKernel.Modules;
using FusionKernel.Runtime;
using FusionKernel.Services;

namespace FusionKernel.Composition;

/// <summary>
/// 提供宿主运行时的最小构造入口。
/// </summary>
public sealed class HostRuntimeBuilder
{
    private readonly FusionHostBuilder _builder = new();
    private readonly InMemoryFusionModuleRegistry _moduleRegistry = new();
    private readonly InMemoryServiceRegistry _serviceRegistry = new();
    private HostCompositionOptions? _options;
    private HostBootstrapContext _bootstrapContext = new();

    /// <summary>
    /// 配置宿主组合选项。
    /// </summary>
    public HostRuntimeBuilder UseOptions(HostCompositionOptions options)
    {
        _options = options;
        return this;
    }

    /// <summary>
    /// 配置宿主引导上下文。
    /// </summary>
    public HostRuntimeBuilder UseBootstrapContext(HostBootstrapContext bootstrapContext)
    {
        _bootstrapContext = bootstrapContext;
        return this;
    }

    /// <summary>
    /// 添加模块实例。
    /// </summary>
    public HostRuntimeBuilder AddModule(IFusionModule module)
    {
        _moduleRegistry.Register(module);
        return this;
    }

    /// <summary>
    /// 构造宿主实例。
    /// </summary>
    public IFusionHost Build()
    {
        var options = _options ?? HostCompositionRoot.CreateDefaultOptions();
        var runtimeContext = new RuntimeContext(
            new RuntimeInstanceId(options.RuntimeInstanceId),
            options.RuntimeRoot,
            options.RunMode,
            options.Profile,
            RuntimeStatus.Created,
            _bootstrapContext.ConfigurationProvider,
            _bootstrapContext.ConfigurationSnapshot,
            _bootstrapContext.LoggerWriter,
            _bootstrapContext.LoggerContext);

        var hostContext = new FusionHostContext(
            options.HostId,
            options.HostName,
            options.RunMode,
            options.RuntimeRoot,
            runtimeContext);

        _builder.UseRuntimeContext(runtimeContext);
        _builder.UseHostContext(hostContext);
        _builder.UseModuleRegistry(_moduleRegistry);
        _builder.UseServiceRegistrar(_serviceRegistry);
        _builder.UseServiceResolver(_serviceRegistry);

        return _builder.Build();
    }
}
