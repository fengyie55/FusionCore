using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Equipment;

/// <summary>
/// 表示设备控制状态发生变化。
/// </summary>
public sealed record ControlStateChangedEvent(
    EquipmentId EquipmentId,
    ControlState PreviousState,
    ControlState CurrentState) : DomainEvent;
