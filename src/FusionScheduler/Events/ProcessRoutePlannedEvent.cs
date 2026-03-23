using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionScheduler.Events;

/// <summary>
/// 表示某个工艺作业的路径规划已生成。
/// </summary>
public sealed record ProcessRoutePlannedEvent(
    ProcessJobId ProcessJobId,
    int DispatchTaskCount) : DomainEvent;
