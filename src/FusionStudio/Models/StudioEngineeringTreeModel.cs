namespace FusionStudio.Models;

/// <summary>
/// 表示 FusionStudio 使用的工程树只读模型。
/// </summary>
public sealed record StudioEngineeringTreeModel(
    string EquipmentName,
    IReadOnlyCollection<StudioEngineeringNodeModel> RootNodes);
