using FusionScheduler.Models;
using FusionScheduler.CoordinationIntents;
using FusionScheduler.PlanningResults;

namespace FusionScheduler.CoordinationInputs;

/// <summary>
/// 表示路径计划骨架进入执行前协调边界的最小输入。
/// </summary>
public sealed record RouteCoordinationInput(
    string CoordinationRequestId,
    string PlanResultId,
    CoordinationPriority Priority,
    DateTimeOffset RequestedAtUtc,
    RoutePlanDecision PlanDecision,
    RoutePlan? RoutePlan)
    : ExecutionCoordinationRequest(
        CoordinationRequestId,
        PlanResultId,
        CoordinationIntents.CoordinationIntentType.Route,
        Priority,
        RequestedAtUtc);
