using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.PlanningIntents;

/// <summary>
/// 表示计划骨架形成后的最小结论分类。
/// </summary>
public enum PlanningConclusionKind
{
    Unknown = 0,
    SkeletonReady = 1,
    NeedCoordination = 2,
    HoldForReview = 3,
    NoPlanGenerated = 4,
}

/// <summary>
/// 提供评估结论到计划结论的最小映射。
/// </summary>
public static class PlanningConclusionKindExtensions
{
    /// <summary>
    /// 将评估结论转换为计划结论。
    /// </summary>
    /// <param name="conclusionKind">评估结论。</param>
    /// <returns>计划结论。</returns>
    public static PlanningConclusionKind ToPlanningConclusionKind(this EvaluationConclusionKind conclusionKind)
    {
        return conclusionKind switch
        {
            EvaluationConclusionKind.NeedFurtherPlanning => PlanningConclusionKind.SkeletonReady,
            EvaluationConclusionKind.NeedFollowUpEvaluation => PlanningConclusionKind.NeedCoordination,
            EvaluationConclusionKind.HoldForReview => PlanningConclusionKind.HoldForReview,
            EvaluationConclusionKind.NoFurtherAction => PlanningConclusionKind.NoPlanGenerated,
            _ => PlanningConclusionKind.Unknown,
        };
    }
}
