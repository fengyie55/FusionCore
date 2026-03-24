using FusionDomain.Events;
using FusionScheduler.EventModels;

namespace FusionScheduler.EventContracts;

/// <summary>
/// 定义面向领域事件的最小消费契约。
/// </summary>
/// <typeparam name="TEvent">领域事件类型。</typeparam>
public interface IDomainEventConsumer<in TEvent>
    where TEvent : DomainEvent
{
    /// <summary>
    /// 消费指定领域事件，并返回调度上下文更新意图。
    /// </summary>
    /// <param name="domainEvent">待消费的领域事件。</param>
    /// <param name="context">消费上下文。</param>
    /// <returns>调度上下文更新结果。</returns>
    SchedulerContextUpdateResult Consume(TEvent domainEvent, ISchedulerEventConsumptionContext context);
}
