using FusionScheduler.PlanningIntents;

namespace FusionScheduler.CoordinationIntents;

/// <summary>
/// 表示执行前协调边界处理的最小意图分类。
/// </summary>
public enum CoordinationIntentType
{
    Unknown = 0,
    Dispatch = 1,
    Route = 2,
    Recovery = 3,
}

/// <summary>
/// 提供计划意图到协调意图的最小映射。
/// </summary>
public static class CoordinationIntentTypeExtensions
{
    /// <summary>
    /// 将计划意图转换为协调意图。
    /// </summary>
    /// <param name="intentType">计划意图。</param>
    /// <returns>协调意图。</returns>
    public static CoordinationIntentType ToCoordinationIntentType(this PlanningIntentType intentType)
    {
        return intentType switch
        {
            PlanningIntentType.Dispatch => CoordinationIntentType.Dispatch,
            PlanningIntentType.Route => CoordinationIntentType.Route,
            PlanningIntentType.Recovery => CoordinationIntentType.Recovery,
            _ => CoordinationIntentType.Unknown,
        };
    }
}
