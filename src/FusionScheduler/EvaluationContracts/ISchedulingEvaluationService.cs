using FusionScheduler.EvaluationModels;
using FusionScheduler.EvaluationResults;
using FusionScheduler.OrchestrationRequests;

namespace FusionScheduler.EvaluationContracts;

/// <summary>
/// 定义内部编排请求进入调度评估边界的最小服务契约。
/// </summary>
public interface ISchedulingEvaluationService
{
    /// <summary>
    /// 对一组内部编排请求执行评估。
    /// </summary>
    /// <param name="requests">待评估的编排请求集合。</param>
    /// <param name="inputContext">评估输入上下文。</param>
    /// <returns>评估结果集合。</returns>
    IReadOnlyCollection<SchedulingEvaluationResult> Evaluate(
        IReadOnlyCollection<SchedulerOrchestrationRequest> requests,
        EvaluationInputContext inputContext);
}
