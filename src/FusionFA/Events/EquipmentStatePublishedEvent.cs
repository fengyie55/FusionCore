using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionFA.Events;

/// <summary>
/// 表示设备状态已对自动化侧发布。
/// </summary>
public sealed record EquipmentStatePublishedEvent(
    EquipmentId EquipmentId,
    string PublishedStateCode) : DomainEvent;
