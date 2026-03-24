using FusionScheduler.EvaluationResults;
using FusionScheduler.PlanningModels;
using FusionScheduler.PlanningResults;

namespace FusionScheduler.PlanningContracts;

/// <summary>
/// 定义单个评估结果到计划骨架结果的最小规划边界。
/// </summary>
public interface IEvaluationResultPlanner
{
    /// <summary>
    /// 基于单个评估结果生成计划骨架结果。
    /// </summary>
    /// <param name="evaluationResult">评估结果。</param>
    /// <param name="inputContext">计划输入上下文。</param>
    /// <returns>计划骨架结果。</returns>
    SchedulingPlanResult Plan(
        SchedulingEvaluationResult evaluationResult,
        PlanningInputContext inputContext);
}
