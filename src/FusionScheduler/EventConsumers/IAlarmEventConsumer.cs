using FusionDomain.Events.Alarm;
using FusionScheduler.EventContracts;

namespace FusionScheduler.EventConsumers;

/// <summary>
/// 定义调度侧关心的告警相关领域事件消费边界。
/// </summary>
public interface IAlarmEventConsumer :
    ISchedulerDomainEventConsumer<AlarmRaisedEvent>,
    ISchedulerDomainEventConsumer<AlarmClearedEvent>
{
}
