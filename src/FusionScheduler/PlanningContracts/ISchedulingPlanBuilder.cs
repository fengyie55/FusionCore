using FusionScheduler.EvaluationResults;
using FusionScheduler.PlanningModels;
using FusionScheduler.PlanningResults;

namespace FusionScheduler.PlanningContracts;

/// <summary>
/// 定义调度评估结果进入计划骨架边界的最小服务契约。
/// </summary>
public interface ISchedulingPlanBuilder
{
    /// <summary>
    /// 基于评估结果集合生成计划骨架结果集合。
    /// </summary>
    /// <param name="evaluationResults">评估结果集合。</param>
    /// <param name="inputContext">计划输入上下文。</param>
    /// <returns>计划骨架结果集合。</returns>
    IReadOnlyCollection<SchedulingPlanResult> Build(
        IReadOnlyCollection<SchedulingEvaluationResult> evaluationResults,
        PlanningInputContext inputContext);
}
