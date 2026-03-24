using FusionDomain.Events.Equipment;
using FusionScheduler.EventContracts;

namespace FusionScheduler.EventConsumers;

/// <summary>
/// 定义调度侧关心的设备相关领域事件消费边界。
/// </summary>
public interface IEquipmentEventConsumer :
    ISchedulerDomainEventConsumer<EquipmentStateChangedEvent>,
    ISchedulerDomainEventConsumer<ControlStateChangedEvent>
{
}
