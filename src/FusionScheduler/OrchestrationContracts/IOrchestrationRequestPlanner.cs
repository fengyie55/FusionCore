using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationModels;
using FusionScheduler.OrchestrationRequests;

namespace FusionScheduler.OrchestrationContracts;

/// <summary>
/// 定义从上下文刷新结果生成内部编排请求的最小规划边界。
/// </summary>
public interface IOrchestrationRequestPlanner
{
    /// <summary>
    /// 基于上下文刷新结果生成后续编排请求。
    /// </summary>
    /// <param name="updateResult">上下文刷新结果。</param>
    /// <param name="inputContext">请求输入上下文。</param>
    /// <returns>编排请求集合。</returns>
    IReadOnlyCollection<SchedulerOrchestrationRequest> Plan(
        SchedulerContextUpdateResult updateResult,
        OrchestrationInputContext inputContext);
}
