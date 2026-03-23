using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Alarm;

/// <summary>
/// 表示告警已发生。
/// </summary>
public sealed record AlarmRaisedEvent(
    AlarmId AlarmId,
    string AlarmCode,
    AlarmSeverity Severity) : DomainEvent;
