using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Material;

/// <summary>
/// 表示基片已被纳入跟踪范围。
/// </summary>
public sealed record SubstrateTrackedEvent(
    SubstrateId SubstrateId,
    MaterialId MaterialId) : DomainEvent;
