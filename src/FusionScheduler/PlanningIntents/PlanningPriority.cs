using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.PlanningIntents;

/// <summary>
/// 表示计划骨架生成阶段的最小优先级语义。
/// </summary>
public enum PlanningPriority
{
    Normal = 0,
    High = 1,
    Critical = 2,
}

/// <summary>
/// 提供评估优先级到计划优先级的最小映射。
/// </summary>
public static class PlanningPriorityExtensions
{
    /// <summary>
    /// 将评估优先级转换为计划优先级。
    /// </summary>
    /// <param name="priority">评估优先级。</param>
    /// <returns>计划优先级。</returns>
    public static PlanningPriority ToPlanningPriority(this EvaluationPriority priority)
    {
        return priority switch
        {
            EvaluationPriority.High => PlanningPriority.High,
            EvaluationPriority.Critical => PlanningPriority.Critical,
            _ => PlanningPriority.Normal,
        };
    }
}
