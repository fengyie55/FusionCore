namespace FusionStudio.Models;

/// <summary>
/// 表示设备总览页需要展示的最小摘要模型。
/// </summary>
public sealed record StudioDeviceOverviewModel(
    string EquipmentName,
    string Summary,
    string RuntimeProfile,
    string RuntimeRoot,
    IReadOnlyCollection<StudioModuleNodeModel> Modules,
    StudioEngineeringTreeModel EngineeringTree);
