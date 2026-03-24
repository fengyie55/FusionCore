using FusionScheduler.Models;
using FusionScheduler.PlanningIntents;

namespace FusionScheduler.PlanningResults;

/// <summary>
/// 表示路径计划骨架相关的最小结论。
/// </summary>
public sealed record RoutePlanDecision(
    RoutePlan? RoutePlan,
    PlanningConclusionKind ConclusionKind,
    string DecisionCode);
