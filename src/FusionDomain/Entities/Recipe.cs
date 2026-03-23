using FusionDomain.Common;
using FusionDomain.ValueObjects;

namespace FusionDomain.Entities;

/// <summary>
/// 表示领域中的配方定义引用。
/// </summary>
public sealed class Recipe : EntityBase<RecipeId>
{
    /// <summary>
    /// 初始化配方实体。
    /// </summary>
    public Recipe(RecipeId id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 获取配方名称。
    /// </summary>
    public string Name { get; }
}
