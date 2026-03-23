using FusionDomain.Events;
using FusionDomain.ValueObjects;

namespace FusionFA.Events;

/// <summary>
/// 表示作业视图已对自动化侧发布。
/// </summary>
public sealed record JobPublishedEvent(
    ControlJobId ControlJobId,
    ProcessJobId ProcessJobId) : DomainEvent;
