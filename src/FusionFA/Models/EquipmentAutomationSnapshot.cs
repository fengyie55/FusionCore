using FusionDomain.ValueObjects;
using FusionFA.States;

namespace FusionFA.Models;

/// <summary>
/// 表示面向工厂自动化侧的设备快照视图。
/// </summary>
public sealed record EquipmentAutomationSnapshot(
    EquipmentId EquipmentId,
    string EquipmentName,
    string EquipmentStateCode,
    string ControlStateCode,
    AutomationConnectionState ConnectionState,
    AutomationAvailabilityState AvailabilityState);
