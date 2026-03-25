namespace FusionKernel.Modules;

/// <summary>
/// 提供模块注册表的最小内存实现。
/// </summary>
public sealed class InMemoryFusionModuleRegistry : IFusionModuleRegistry
{
    private readonly Dictionary<string, IFusionModule> _modulesById = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, string> _moduleIdsByName = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, ModuleState> _statesById = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 注册模块实例。
    /// </summary>
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
        _statesById[moduleId] = ModuleState.Registered;

        if (!string.IsNullOrWhiteSpace(moduleName))
        {
            _moduleIdsByName[moduleName] = moduleId;
        }

        return new ModuleRegistrationResult(true, moduleId, null);
    }

    /// <summary>
    /// 获取已注册模块描述集合。
    /// </summary>
    public IReadOnlyCollection<IFusionModuleDescriptor> GetRegisteredModules()
    {
        return _modulesById.Values.Select(module => module.Descriptor).ToArray();
    }

    /// <summary>
    /// 获取已注册模块实例集合。
    /// </summary>
    public IReadOnlyCollection<IFusionModule> GetModules()
    {
        return _modulesById.Values.ToArray();
    }

    /// <summary>
    /// 按模块标识尝试获取模块。
    /// </summary>
    public bool TryGetModule(string moduleId, out IFusionModule? module)
    {
        var found = _modulesById.TryGetValue(moduleId, out var registeredModule);
        module = registeredModule;
        return found;
    }

    /// <summary>
    /// 按模块名称尝试获取模块。
    /// </summary>
    public bool TryGetModuleByName(string moduleName, out IFusionModule? module)
    {
        module = null;

        if (!_moduleIdsByName.TryGetValue(moduleName, out var moduleId))
        {
            return false;
        }

        return TryGetModule(moduleId, out module);
    }

    /// <summary>
    /// 获取模块当前状态。
    /// </summary>
    public ModuleState GetModuleState(string moduleId)
    {
        return _statesById.TryGetValue(moduleId, out var state)
            ? state
            : ModuleState.Registered;
    }

    /// <summary>
    /// 更新模块状态。
    /// </summary>
    public bool TryUpdateState(string moduleId, ModuleState state)
    {
        if (!_modulesById.ContainsKey(moduleId))
        {
            return false;
        }

        _statesById[moduleId] = state;
        return true;
    }

    /// <summary>
    /// 创建模块集合快照。
    /// </summary>
    public ModuleCollectionSnapshot CreateSnapshot()
    {
        return new ModuleCollectionSnapshot(
            GetRegisteredModules(),
            new Dictionary<string, ModuleState>(_statesById, StringComparer.OrdinalIgnoreCase));
    }
}
