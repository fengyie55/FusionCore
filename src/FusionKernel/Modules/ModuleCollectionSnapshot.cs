namespace FusionKernel.Modules;

/// <summary>
/// 表示模块集合快照。
/// </summary>
public sealed record ModuleCollectionSnapshot(
    IReadOnlyCollection<IFusionModuleDescriptor> Modules,
    IReadOnlyDictionary<string, ModuleState> States);
