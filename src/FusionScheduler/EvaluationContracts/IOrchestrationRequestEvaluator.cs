using FusionScheduler.EvaluationModels;
using FusionScheduler.EvaluationResults;
using FusionScheduler.OrchestrationRequests;

namespace FusionScheduler.EvaluationContracts;

/// <summary>
/// 定义单个内部编排请求的最小评估边界。
/// </summary>
public interface IOrchestrationRequestEvaluator
{
    /// <summary>
    /// 评估单个内部编排请求。
    /// </summary>
    /// <param name="request">待评估的编排请求。</param>
    /// <param name="inputContext">评估输入上下文。</param>
    /// <returns>评估结果。</returns>
    SchedulingEvaluationResult Evaluate(
        SchedulerOrchestrationRequest request,
        EvaluationInputContext inputContext);
}
