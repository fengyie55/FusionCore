using FusionScheduler.PlanningIntents;
using FusionScheduler.PlanningModels;

namespace FusionScheduler.PlanningResults;

/// <summary>
/// 表示计划骨架边界输出的最小结果对象。
/// </summary>
public sealed record SchedulingPlanResult(
    string PlanResultId,
    string EvaluationResultId,
    PlanningIntentType IntentType,
    PlanningPriority Priority,
    PlanningBasisReference BasisReference,
    PlanningSummary Summary,
    DispatchPlanDecision? DispatchPlanDecision,
    RoutePlanDecision? RoutePlanDecision,
    RecoveryPlanDecision? RecoveryPlanDecision);
