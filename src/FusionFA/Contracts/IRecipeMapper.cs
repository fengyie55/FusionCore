using FusionDomain.Entities;
using FusionFA.Models;

namespace FusionFA.Contracts;

/// <summary>
/// 定义领域配方到自动化配方视图的映射边界。
/// </summary>
public interface IRecipeMapper
{
    /// <summary>
    /// 将领域配方映射为自动化配方视图。
    /// </summary>
    AutomationRecipeView Map(Recipe recipe);
}
