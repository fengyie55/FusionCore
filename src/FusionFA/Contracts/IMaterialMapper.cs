using FusionDomain.Entities;
using FusionFA.Models;

namespace FusionFA.Contracts;

/// <summary>
/// 定义领域物料到自动化物料视图的映射边界。
/// </summary>
public interface IMaterialMapper
{
    /// <summary>
    /// 将领域物料映射为自动化物料视图。
    /// </summary>
    AutomationMaterialView Map(Material material);
}
