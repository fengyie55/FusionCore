using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionFA.Events;

/// <summary>
/// 表示配方视图已对自动化侧发布。
/// </summary>
public sealed record RecipePublishedEvent(
    RecipeId RecipeId,
    string RecipeName) : DomainEvent;
