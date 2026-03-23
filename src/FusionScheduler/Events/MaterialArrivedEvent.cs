using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionScheduler.Events;

/// <summary>
/// 表示受跟踪物料已到达调度层可见的位置。
/// </summary>
public sealed record MaterialArrivedEvent(
    MaterialId MaterialId,
    ModuleId? ModuleId,
    string? Location) : DomainEvent;
