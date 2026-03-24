using FusionScheduler.Models;
using FusionScheduler.CoordinationIntents;
using FusionScheduler.PlanningResults;

namespace FusionScheduler.CoordinationInputs;

/// <summary>
/// 表示派发计划骨架进入执行前协调边界的最小输入。
/// </summary>
public sealed record DispatchCoordinationInput(
    string CoordinationRequestId,
    string PlanResultId,
    CoordinationPriority Priority,
    DateTimeOffset RequestedAtUtc,
    DispatchPlanDecision PlanDecision,
    IReadOnlyList<DispatchTask> DispatchTasks)
    : ExecutionCoordinationRequest(
        CoordinationRequestId,
        PlanResultId,
        CoordinationIntents.CoordinationIntentType.Dispatch,
        Priority,
        RequestedAtUtc);
