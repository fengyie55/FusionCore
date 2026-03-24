using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.PlanningIntents;

/// <summary>
/// 表示计划骨架边界处理的最小意图分类。
/// </summary>
public enum PlanningIntentType
{
    Unknown = 0,
    Dispatch = 1,
    Route = 2,
    Recovery = 3,
}

/// <summary>
/// 提供评估意图到计划意图的最小映射。
/// </summary>
public static class PlanningIntentTypeExtensions
{
    /// <summary>
    /// 将评估意图转换为计划意图。
    /// </summary>
    /// <param name="intentType">评估意图。</param>
    /// <returns>计划意图。</returns>
    public static PlanningIntentType ToPlanningIntentType(this EvaluationIntentType intentType)
    {
        return intentType switch
        {
            EvaluationIntentType.Route => PlanningIntentType.Route,
            EvaluationIntentType.MaterialFlow => PlanningIntentType.Dispatch,
            EvaluationIntentType.JobProgress => PlanningIntentType.Dispatch,
            EvaluationIntentType.Recovery => PlanningIntentType.Recovery,
            _ => PlanningIntentType.Unknown,
        };
    }
}
