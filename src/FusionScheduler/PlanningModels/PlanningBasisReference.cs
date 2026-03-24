using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.PlanningModels;

/// <summary>
/// 表示计划骨架所依据的最小评估引用信息。
/// </summary>
public sealed record PlanningBasisReference(
    string EvaluationResultId,
    string RequestId,
    EvaluationConclusionKind ConclusionKind);
