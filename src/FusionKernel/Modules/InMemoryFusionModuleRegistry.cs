namespace FusionKernel.Modules;

/// <summary>
/// 提供模块注册表的最小内存实现。
/// </summary>
public sealed class InMemoryFusionModuleRegistry : IFusionModuleRegistry
{
    private readonly Dictionary<string, IFusionModule> _modulesById = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _moduleIdsByName = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 注册模块实例。
    /// </summary>
    /// <param name="module">待注册模块。</param>
    /// <returns>注册结果。</returns>
    public ModuleRegistrationResult Register(IFusionModule module)
    {
        ArgumentNullException.ThrowIfNull(module);

        var moduleId = module.Descriptor.ModuleId;
        var moduleName = module.Descriptor.ModuleName;

        if (string.IsNullOrWhiteSpace(moduleId))
        {
            return new ModuleRegistrationResult(false, string.Empty, "模块标识不能为空。");
        }

        if (_modulesById.ContainsKey(moduleId))
        {
            return new ModuleRegistrationResult(false, moduleId, "模块标识已注册。");
        }

        if (!string.IsNullOrWhiteSpace(moduleName) && _moduleIdsByName.ContainsKey(moduleName))
        {
            return new ModuleRegistrationResult(false, moduleId, "模块名称已注册。");
        }

        _modulesById[moduleId] = module;

        if (!string.IsNullOrWhiteSpace(moduleName))
        {
            _moduleIdsByName[moduleName] = moduleId;
        }

        return new ModuleRegistrationResult(true, moduleId, null);
    }

    /// <summary>
    /// 获取已注册模块描述集合。
    /// </summary>
    /// <returns>模块描述集合。</returns>
    public IReadOnlyCollection<IFusionModuleDescriptor> GetRegisteredModules()
    {
        return _modulesById.Values
            .Select(module => module.Descriptor)
            .ToArray();
    }

    /// <summary>
    /// 按模块标识尝试获取模块。
    /// </summary>
    /// <param name="moduleId">模块标识。</param>
    /// <param name="module">模块实例。</param>
    /// <returns>是否找到模块。</returns>
    public bool TryGetModule(string moduleId, out IFusionModule? module)
    {
        var found = _modulesById.TryGetValue(moduleId, out var registeredModule);
        module = registeredModule;
        return found;
    }

    /// <summary>
    /// 按模块名称尝试获取模块。
    /// </summary>
    /// <param name="moduleName">模块名称。</param>
    /// <param name="module">模块实例。</param>
    /// <returns>是否找到模块。</returns>
    public bool TryGetModuleByName(string moduleName, out IFusionModule? module)
    {
        module = null;

        if (!_moduleIdsByName.TryGetValue(moduleName, out var moduleId))
        {
            return false;
        }

        return TryGetModule(moduleId, out module);
    }
}
