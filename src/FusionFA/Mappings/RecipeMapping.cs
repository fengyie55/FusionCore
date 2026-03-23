using FusionDomain.ValueObjects;

namespace FusionFA.Mappings;

/// <summary>
/// 表示领域配方到自动化配方视图的映射描述。
/// </summary>
public sealed record RecipeMapping(
    RecipeId RecipeId,
    string RecipeName,
    string PublishedStateCode);
