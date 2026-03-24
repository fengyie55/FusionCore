namespace FusionScheduler.EvaluationIntents;

/// <summary>
/// 表示调度评估得出的最小结论分类。
/// </summary>
public enum EvaluationConclusionKind
{
    Unknown = 0,
    NeedFurtherPlanning = 1,
    NeedFollowUpEvaluation = 2,
    HoldForReview = 3,
    NoFurtherAction = 4,
}
