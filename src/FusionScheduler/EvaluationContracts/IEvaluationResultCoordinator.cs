using FusionScheduler.EvaluationResults;

namespace FusionScheduler.EvaluationContracts;

/// <summary>
/// 定义对评估结果集合进行归并与输出整理的最小协作契约。
/// </summary>
public interface IEvaluationResultCoordinator
{
    /// <summary>
    /// 协调一组评估结果。
    /// </summary>
    /// <param name="results">待协调的评估结果集合。</param>
    /// <returns>协调后的评估结果集合。</returns>
    IReadOnlyCollection<SchedulingEvaluationResult> Coordinate(
        IReadOnlyCollection<SchedulingEvaluationResult> results);
}
