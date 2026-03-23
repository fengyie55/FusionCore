using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Recipe;

/// <summary>
/// 表示配方已分配给工艺作业。
/// </summary>
public sealed record RecipeAssignedEvent(
    RecipeId RecipeId,
    ProcessJobId ProcessJobId) : DomainEvent;
