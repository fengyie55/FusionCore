using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Job;

/// <summary>
/// 表示控制作业状态发生变化。
/// </summary>
public sealed record ControlJobStateChangedEvent(
    ControlJobId ControlJobId,
    ControlState PreviousState,
    ControlState CurrentState) : DomainEvent;
