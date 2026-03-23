using FusionDomain.Events.Common;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Carrier;

/// <summary>
/// 表示载具已装载到某个位置。
/// </summary>
public sealed record CarrierLoadedEvent(
    CarrierId CarrierId,
    EquipmentId EquipmentId,
    LocationReference Location) : DomainEvent;
