using FusionDomain.Events.Job;
using FusionDomain.Events.Recipe;
using FusionScheduler.EventModels;

namespace FusionScheduler.EventUpdates;

/// <summary>
/// 定义生产作业上下文相关的事件更新边界。
/// </summary>
public interface IProductionJobContextUpdater
{
    /// <summary>
    /// 基于控制作业创建事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(ControlJobCreatedEvent domainEvent);

    /// <summary>
    /// 基于控制作业状态变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(ControlJobStateChangedEvent domainEvent);

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

    /// <summary>
    /// 基于配方变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(RecipeChangedEvent domainEvent);
}
