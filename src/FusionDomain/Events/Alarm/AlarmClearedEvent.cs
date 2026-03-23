using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Alarm;

/// <summary>
/// 表示告警已清除。
/// </summary>
public sealed record AlarmClearedEvent(
    AlarmId AlarmId,
    string AlarmCode) : DomainEvent;
