using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionFA.Events;

/// <summary>
/// 表示告警视图已对自动化侧发布。
/// </summary>
public sealed record AlarmPublishedEvent(
    AlarmId AlarmId,
    string AlarmCode) : DomainEvent;
