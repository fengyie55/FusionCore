using FusionDomain.Common;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Aggregates;

/// <summary>
/// 表示与工艺处理语义对齐的工艺作业聚合根。
/// </summary>
public sealed class ProcessJob : EntityBase<ProcessJobId>
{
    /// <summary>
    /// 初始化工艺作业聚合根。
    /// </summary>
    public ProcessJob(ProcessJobId id, ControlState state, RecipeId recipeId)
        : base(id)
    {
        State = state;
        RecipeId = recipeId;
    }

    /// <summary>
    /// 获取当前工艺作业状态。
    /// </summary>
    public ControlState State { get; }

    /// <summary>
    /// 获取引用的配方标识。
    /// </summary>
    public RecipeId RecipeId { get; }
}
