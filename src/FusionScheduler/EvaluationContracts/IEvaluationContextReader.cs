using FusionScheduler.EvaluationModels;
using FusionScheduler.OrchestrationRequests;

namespace FusionScheduler.EvaluationContracts;

/// <summary>
/// 定义调度评估阶段使用的最小只读上下文访问边界。
/// </summary>
public interface IEvaluationContextReader
{
    /// <summary>
    /// 为指定编排请求读取评估输入上下文。
    /// </summary>
    /// <param name="request">待评估的编排请求。</param>
    /// <returns>评估输入上下文。</returns>
    EvaluationInputContext ReadFor(SchedulerOrchestrationRequest request);
}
