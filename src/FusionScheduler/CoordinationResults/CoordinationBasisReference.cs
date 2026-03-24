using FusionScheduler.PlanningIntents;

namespace FusionScheduler.CoordinationResults;

/// <summary>
/// 表示执行前协调结果所依据的最小计划引用信息。
/// </summary>
public sealed record CoordinationBasisReference(
    string PlanResultId,
    string EvaluationResultId,
    PlanningConclusionKind ConclusionKind);
