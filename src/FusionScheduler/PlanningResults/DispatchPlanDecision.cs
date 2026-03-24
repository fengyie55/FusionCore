using FusionScheduler.Models;
using FusionScheduler.PlanningIntents;

namespace FusionScheduler.PlanningResults;

/// <summary>
/// 表示派发计划骨架相关的最小结论。
/// </summary>
public sealed record DispatchPlanDecision(
    IReadOnlyList<DispatchTask> DispatchTasks,
    PlanningConclusionKind ConclusionKind,
    string DecisionCode);
