using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationModels;
using FusionScheduler.OrchestrationRequests;

namespace FusionScheduler.OrchestrationContracts;

/// <summary>
/// 定义将上下文刷新结果推进到内部编排请求边界的最小协作契约。
/// </summary>
public interface IContextRefreshOrchestrator
{
    /// <summary>
    /// 基于刷新结果生成编排请求集合。
    /// </summary>
    /// <param name="updateResult">上下文刷新结果。</param>
    /// <param name="inputContext">请求输入上下文。</param>
    /// <returns>编排请求集合。</returns>
    IReadOnlyCollection<SchedulerOrchestrationRequest> Orchestrate(
        SchedulerContextUpdateResult updateResult,
        OrchestrationInputContext inputContext);
}
