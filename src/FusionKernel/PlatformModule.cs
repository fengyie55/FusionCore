using FusionKernel.Modules;
using FusionKernel.Results;
using FusionKernel.Services;

namespace FusionKernel;

/// <summary>
/// 提供 FusionKernel 的最小占位模块实现。
/// </summary>
public sealed class PlatformModule : IFusionModule, IModuleLifecycle
{
    /// <summary>
    /// 获取模块标识。
    /// </summary>
    public string Id => "FusionKernel.PlatformModule";

    /// <summary>
    /// 获取模块名称。
    /// </summary>
    public string Name => nameof(PlatformModule);

    /// <summary>
    /// 获取模块描述。
    /// </summary>
    public IFusionModuleDescriptor Descriptor { get; } = new ModuleDescriptor(
        "FusionKernel.PlatformModule",
        "FusionKernel Platform Module",
        "1.0.0");

    /// <summary>
    /// 执行最小初始化。
    /// </summary>
    public void Initialize()
    {
    }

    /// <summary>
    /// 配置模块服务。
    /// </summary>
    public void ConfigureServices(IServiceRegistrar registrar)
    {
    }

    /// <summary>
    /// 执行模块初始化阶段。
    /// </summary>
    public ModuleInitializationResult InitializeModule(ModuleInitializationContext context)
    {
        Initialize();
        ConfigureServices(context.ServiceRegistrar);
        return new ModuleInitializationResult(true, Descriptor.ModuleId, "MODULE_INITIALIZED", null);
    }

    /// <summary>
    /// 执行模块启动阶段。
    /// </summary>
    public ModuleStartResult StartModule(ModuleStartContext context)
    {
        return new ModuleStartResult(true, Descriptor.ModuleId, "MODULE_STARTED", null);
    }

    /// <summary>
    /// 执行模块停止阶段。
    /// </summary>
    public ModuleStopResult StopModule(ModuleStopContext context)
    {
        return new ModuleStopResult(true, Descriptor.ModuleId, "MODULE_STOPPED", null);
    }
}
