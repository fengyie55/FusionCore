namespace FusionKernel.Modules;

/// <summary>
/// 定义模块注册边界的最小契约。
/// </summary>
public interface IFusionModuleRegistry
{
    /// <summary>
    /// 注册模块。
    /// </summary>
    /// <param name="module">待注册模块。</param>
    /// <returns>注册结果。</returns>
    ModuleRegistrationResult Register(IFusionModule module);

    /// <summary>
    /// 获取当前已注册模块描述集合。
    /// </summary>
    /// <returns>模块描述集合。</returns>
    IReadOnlyCollection<IFusionModuleDescriptor> GetRegisteredModules();

    /// <summary>
    /// 按模块标识尝试获取已注册模块。
    /// </summary>
    /// <param name="moduleId">模块标识。</param>
    /// <param name="module">已注册模块。</param>
    /// <returns>是否找到对应模块。</returns>
    bool TryGetModule(string moduleId, out IFusionModule? module);

    /// <summary>
    /// 按模块名称尝试获取已注册模块。
    /// </summary>
    /// <param name="moduleName">模块名称。</param>
    /// <param name="module">已注册模块。</param>
    /// <returns>是否找到对应模块。</returns>
    bool TryGetModuleByName(string moduleName, out IFusionModule? module);
}
