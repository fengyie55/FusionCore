using FusionScheduler.Models;
using FusionScheduler.CoordinationIntents;
using FusionScheduler.PlanningResults;

namespace FusionScheduler.CoordinationInputs;

/// <summary>
/// 表示恢复计划骨架进入执行前协调边界的最小输入。
/// </summary>
public sealed record RecoveryCoordinationInput(
    string CoordinationRequestId,
    string PlanResultId,
    CoordinationPriority Priority,
    DateTimeOffset RequestedAtUtc,
    RecoveryPlanDecision PlanDecision,
    RecoveryPlan? RecoveryPlan)
    : ExecutionCoordinationRequest(
        CoordinationRequestId,
        PlanResultId,
        CoordinationIntents.CoordinationIntentType.Recovery,
        Priority,
        RequestedAtUtc);
