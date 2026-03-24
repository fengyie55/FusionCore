using FusionDomain.Events.Job;
using FusionScheduler.EventContracts;

namespace FusionScheduler.EventConsumers;

/// <summary>
/// 定义调度侧关心的作业相关领域事件消费边界。
/// </summary>
public interface IJobEventConsumer :
    ISchedulerDomainEventConsumer<ControlJobCreatedEvent>,
    ISchedulerDomainEventConsumer<ControlJobStateChangedEvent>,
    ISchedulerDomainEventConsumer<ProcessJobCreatedEvent>,
    ISchedulerDomainEventConsumer<ProcessJobStateChangedEvent>
{
}
