using FusionStudio.Navigation;

namespace FusionStudio.Models;

/// <summary>
/// 表示工程树中的只读节点摘要。
/// </summary>
public sealed record StudioEngineeringNodeModel(
    string NodeId,
    string Title,
    StudioEngineeringNodeKind Kind,
    string Summary,
    string? State,
    StudioRoute Route,
    IReadOnlyCollection<StudioEngineeringNodeModel> Children);
