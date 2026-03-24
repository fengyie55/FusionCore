using FusionDomain.Events.Carrier;
using FusionDomain.Events.Equipment;
using FusionDomain.Events.Job;
using FusionDomain.Events.Material;
using FusionDomain.Events.Recipe;
using FusionScheduler.EventModels;

namespace FusionScheduler.EventUpdates;

/// <summary>
/// 定义路由上下文相关的事件更新边界。
/// </summary>
public interface IRouteContextUpdater
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
    /// 基于物料位置变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(MaterialLocationChangedEvent domainEvent);

    /// <summary>
    /// 基于载具装载事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(CarrierLoadedEvent domainEvent);

    /// <summary>
    /// 基于载具卸载事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(CarrierUnloadedEvent domainEvent);

    /// <summary>
    /// 基于工艺作业创建事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(ProcessJobCreatedEvent domainEvent);

    /// <summary>
    /// 基于工艺作业状态变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(ProcessJobStateChangedEvent domainEvent);

    /// <summary>
    /// 基于配方分配事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(RecipeAssignedEvent domainEvent);
}
