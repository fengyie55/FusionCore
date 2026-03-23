using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionFA.Events;

/// <summary>
/// 表示物料视图已对自动化侧发布。
/// </summary>
public sealed record MaterialPublishedEvent(
    MaterialId MaterialId,
    string MaterialType) : DomainEvent;
