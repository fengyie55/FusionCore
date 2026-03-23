using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Material;

/// <summary>
/// 表示物料状态发生变化。
/// </summary>
public sealed record MaterialStateChangedEvent(
    MaterialId MaterialId,
    MaterialState PreviousState,
    MaterialState CurrentState) : DomainEvent;
