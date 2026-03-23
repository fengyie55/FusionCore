using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Job;

/// <summary>
/// 表示工艺作业状态发生变化。
/// </summary>
public sealed record ProcessJobStateChangedEvent(
    ProcessJobId ProcessJobId,
    ControlState PreviousState,
    ControlState CurrentState) : DomainEvent;
