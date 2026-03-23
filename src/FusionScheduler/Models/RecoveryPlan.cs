using FusionDomain.ValueObjects;
using FusionScheduler.Recovery;

namespace FusionScheduler.Models;

/// <summary>
/// 表示工艺或物料流中断时的最小恢复计划。
/// </summary>
public sealed record RecoveryPlan(
    ProcessJobId ProcessJobId,
    RecoveryReason Reason,
    IReadOnlyList<RecoveryActionType> Actions);
