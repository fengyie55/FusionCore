namespace FusionKernel.Modules;

/// <summary>
/// 定义模块注册边界的最小契约。
/// </summary>
public interface IFusionModuleRegistry
{
    /// <summary>
    /// 注册模块。
    /// </summary>
    ModuleRegistrationResult Register(IFusionModule module);

    /// <summary>
    /// 获取当前已注册模块描述集合。
    /// </summary>
    IReadOnlyCollection<IFusionModuleDescriptor> GetRegisteredModules();

    /// <summary>
    /// 获取已注册模块实例集合。
    /// </summary>
    IReadOnlyCollection<IFusionModule> GetModules();

    /// <summary>
    /// 按模块标识尝试获取已注册模块。
    /// </summary>
    bool TryGetModule(string moduleId, out IFusionModule? module);

    /// <summary>
    /// 按模块名称尝试获取已注册模块。
    /// </summary>
    bool TryGetModuleByName(string moduleName, out IFusionModule? module);

    /// <summary>
    /// 获取模块当前状态。
    /// </summary>
    ModuleState GetModuleState(string moduleId);

    /// <summary>
    /// 更新模块状态。
    /// </summary>
    bool TryUpdateState(string moduleId, ModuleState state);

    /// <summary>
    /// 创建模块集合快照。
    /// </summary>
    ModuleCollectionSnapshot CreateSnapshot();
}
