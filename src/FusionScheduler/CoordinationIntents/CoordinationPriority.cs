using FusionScheduler.PlanningIntents;

namespace FusionScheduler.CoordinationIntents;

/// <summary>
/// 表示执行前协调阶段的最小优先级语义。
/// </summary>
public enum CoordinationPriority
{
    Normal = 0,
    High = 1,
    Critical = 2,
}

/// <summary>
/// 提供计划优先级到协调优先级的最小映射。
/// </summary>
public static class CoordinationPriorityExtensions
{
    /// <summary>
    /// 将计划优先级转换为协调优先级。
    /// </summary>
    /// <param name="priority">计划优先级。</param>
    /// <returns>协调优先级。</returns>
    public static CoordinationPriority ToCoordinationPriority(this PlanningPriority priority)
    {
        return priority switch
        {
            PlanningPriority.High => CoordinationPriority.High,
            PlanningPriority.Critical => CoordinationPriority.Critical,
            _ => CoordinationPriority.Normal,
        };
    }
}
