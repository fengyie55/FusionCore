using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Recipe;

/// <summary>
/// 表示配方引用已发生变化。
/// </summary>
public sealed record RecipeChangedEvent(
    RecipeId PreviousRecipeId,
    RecipeId CurrentRecipeId,
    ProcessJobId ProcessJobId) : DomainEvent;
