using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Job;

/// <summary>
/// 表示控制作业已创建。
/// </summary>
public sealed record ControlJobCreatedEvent(
    ControlJobId ControlJobId) : DomainEvent;
