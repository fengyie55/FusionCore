using FusionKernel.Results;

namespace FusionKernel.Modules;

/// <summary>
/// 定义模块生命周期协调边界。
/// </summary>
public interface IModuleLifecycle
{
    /// <summary>
    /// 执行模块初始化阶段。
    /// </summary>
    ModuleInitializationResult InitializeModule(ModuleInitializationContext context);

    /// <summary>
    /// 执行模块启动阶段。
    /// </summary>
    ModuleStartResult StartModule(ModuleStartContext context);

    /// <summary>
    /// 执行模块停止阶段。
    /// </summary>
    ModuleStopResult StopModule(ModuleStopContext context);
}
