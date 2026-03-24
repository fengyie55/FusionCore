using FusionDomain.Events;

namespace FusionScheduler.EventContracts;

/// <summary>
/// 标识该消费者属于调度侧的领域事件消费边界。
/// </summary>
/// <typeparam name="TEvent">领域事件类型。</typeparam>
public interface ISchedulerDomainEventConsumer<in TEvent> : IDomainEventConsumer<TEvent>
    where TEvent : DomainEvent
{
}
