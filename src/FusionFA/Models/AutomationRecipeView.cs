using FusionDomain.ValueObjects;

namespace FusionFA.Models;

/// <summary>
/// 表示面向自动化侧发布的配方视图。
/// </summary>
public sealed record AutomationRecipeView(
    RecipeId RecipeId,
    string RecipeName,
    string RecipeStateCode);
