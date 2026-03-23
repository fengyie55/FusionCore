using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Material;

/// <summary>
/// 表示物料已创建并进入领域语义范围。
/// </summary>
public sealed record MaterialCreatedEvent(
    MaterialId MaterialId,
    MaterialState InitialState) : DomainEvent;
