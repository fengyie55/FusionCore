using FusionDomain.Events.Common;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Carrier;

/// <summary>
/// 表示载具已从某个位置卸载。
/// </summary>
public sealed record CarrierUnloadedEvent(
    CarrierId CarrierId,
    EquipmentId EquipmentId,
    LocationReference? Location) : DomainEvent;
