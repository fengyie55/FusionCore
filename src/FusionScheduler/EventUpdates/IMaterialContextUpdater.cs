using FusionDomain.Events.Material;
using FusionScheduler.EventModels;

namespace FusionScheduler.EventUpdates;

/// <summary>
/// 定义物料上下文相关的事件更新边界。
/// </summary>
public interface IMaterialContextUpdater
{
    /// <summary>
    /// 基于物料创建事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(MaterialCreatedEvent domainEvent);

    /// <summary>
    /// 基于物料状态变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(MaterialStateChangedEvent domainEvent);

    /// <summary>
    /// 基于物料位置变化事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(MaterialLocationChangedEvent domainEvent);

    /// <summary>
    /// 基于基片纳入跟踪事实生成更新意图。
    /// </summary>
    SchedulerContextUpdateResult CreateUpdate(SubstrateTrackedEvent domainEvent);
}
