using FusionDomain.Events.Alarm;
using FusionDomain.Events.Equipment;
using FusionScheduler.EventModels;

namespace FusionScheduler.EventUpdates;

/// <summary>
/// 定义恢复评估上下文相关的事件更新边界。
/// </summary>
public interface IRecoveryContextUpdater
{
    /// <summary>
    /// 基于设备状态变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(EquipmentStateChangedEvent domainEvent);

    /// <summary>
    /// 基于控制状态变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(ControlStateChangedEvent domainEvent);

    /// <summary>
    /// 基于告警触发事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(AlarmRaisedEvent domainEvent);

    /// <summary>
    /// 基于告警清除事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(AlarmClearedEvent domainEvent);
}
