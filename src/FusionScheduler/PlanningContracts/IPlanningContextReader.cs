using FusionScheduler.EvaluationResults;
using FusionScheduler.PlanningModels;

namespace FusionScheduler.PlanningContracts;

/// <summary>
/// 定义计划骨架阶段使用的最小只读上下文访问边界。
/// </summary>
public interface IPlanningContextReader
{
    /// <summary>
    /// 为指定评估结果读取计划输入上下文。
    /// </summary>
    /// <param name="evaluationResult">评估结果。</param>
    /// <returns>计划输入上下文。</returns>
    PlanningInputContext ReadFor(SchedulingEvaluationResult evaluationResult);
}
