using FusionDomain.Events.Material;
using FusionScheduler.EventContracts;

namespace FusionScheduler.EventConsumers;

/// <summary>
/// 定义调度侧关心的物料与基片相关领域事件消费边界。
/// </summary>
public interface IMaterialEventConsumer :
    ISchedulerDomainEventConsumer<MaterialCreatedEvent>,
    ISchedulerDomainEventConsumer<MaterialStateChangedEvent>,
    ISchedulerDomainEventConsumer<MaterialLocationChangedEvent>,
    ISchedulerDomainEventConsumer<SubstrateTrackedEvent>
{
}
