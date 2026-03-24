using FusionScheduler.CoordinationResults;

namespace FusionScheduler.CoordinationContracts;

/// <summary>
/// 定义执行前协调结果集合的最小归并契约。
/// </summary>
public interface ICoordinationResultAggregator
{
    /// <summary>
    /// 对协调结果集合进行归并整理。
    /// </summary>
    /// <param name="results">协调结果集合。</param>
    /// <returns>归并后的协调结果集合。</returns>
    IReadOnlyCollection<CoordinationResult> Aggregate(
        IReadOnlyCollection<CoordinationResult> results);
}
