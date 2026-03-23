using FusionDomain.Events.Common;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Material;

/// <summary>
/// 表示物料位置发生变化。
/// </summary>
public sealed record MaterialLocationChangedEvent(
    MaterialId MaterialId,
    LocationReference? PreviousLocation,
    LocationReference? CurrentLocation) : DomainEvent;
