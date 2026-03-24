using FusionDomain.Events.Carrier;
using FusionScheduler.EventContracts;

namespace FusionScheduler.EventConsumers;

/// <summary>
/// 定义调度侧关心的载具相关领域事件消费边界。
/// </summary>
public interface ICarrierEventConsumer :
    ISchedulerDomainEventConsumer<CarrierLoadedEvent>,
    ISchedulerDomainEventConsumer<CarrierUnloadedEvent>
{
}
