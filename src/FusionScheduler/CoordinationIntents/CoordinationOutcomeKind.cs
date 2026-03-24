using FusionScheduler.PlanningIntents;

namespace FusionScheduler.CoordinationIntents;

/// <summary>
/// 表示执行前协调后的最小结果分类。
/// </summary>
public enum CoordinationOutcomeKind
{
    Unknown = 0,
    ReadyForExecutionPreparation = 1,
    NeedConflictResolution = 2,
    NeedPrecheck = 3,
    HoldForReview = 4,
}

/// <summary>
/// 提供计划结论到协调结果的最小映射。
/// </summary>
public static class CoordinationOutcomeKindExtensions
{
    /// <summary>
    /// 将计划结论转换为协调结果。
    /// </summary>
    /// <param name="conclusionKind">计划结论。</param>
    /// <returns>协调结果。</returns>
    public static CoordinationOutcomeKind ToCoordinationOutcomeKind(this PlanningConclusionKind conclusionKind)
    {
        return conclusionKind switch
        {
            PlanningConclusionKind.SkeletonReady => CoordinationOutcomeKind.ReadyForExecutionPreparation,
            PlanningConclusionKind.NeedCoordination => CoordinationOutcomeKind.NeedConflictResolution,
            PlanningConclusionKind.HoldForReview => CoordinationOutcomeKind.HoldForReview,
            PlanningConclusionKind.NoPlanGenerated => CoordinationOutcomeKind.NeedPrecheck,
            _ => CoordinationOutcomeKind.Unknown,
        };
    }
}
