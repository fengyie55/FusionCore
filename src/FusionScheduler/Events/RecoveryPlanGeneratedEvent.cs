using FusionDomain.Events;
using FusionDomain.ValueObjects;
using FusionScheduler.Recovery;

namespace FusionScheduler.Events;

/// <summary>
/// 表示某个工艺作业的恢复计划已生成。
/// </summary>
public sealed record RecoveryPlanGeneratedEvent(
    ProcessJobId ProcessJobId,
    RecoveryReason Reason,
    int ActionCount) : DomainEvent;
