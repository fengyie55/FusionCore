using FusionScheduler.Models;
using FusionScheduler.PlanningIntents;

namespace FusionScheduler.PlanningResults;

/// <summary>
/// 表示恢复计划骨架相关的最小结论。
/// </summary>
public sealed record RecoveryPlanDecision(
    RecoveryPlan? RecoveryPlan,
    PlanningConclusionKind ConclusionKind,
    string DecisionCode);
