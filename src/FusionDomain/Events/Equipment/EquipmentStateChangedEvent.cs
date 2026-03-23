using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Equipment;

/// <summary>
/// 表示设备状态发生变化。
/// </summary>
public sealed record EquipmentStateChangedEvent(
    EquipmentId EquipmentId,
    EquipmentState PreviousState,
    EquipmentState CurrentState) : DomainEvent;
