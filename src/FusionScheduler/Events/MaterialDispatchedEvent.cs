using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionScheduler.Events;

/// <summary>
/// 表示调度层已为某个物料发出派发指令。
/// </summary>
public sealed record MaterialDispatchedEvent(
    MaterialId MaterialId,
    string DispatchTaskId,
    ModuleId? TargetModuleId) : DomainEvent;
