using FusionScheduler.PlanningResults;

namespace FusionScheduler.PlanningContracts;

/// <summary>
/// 定义对计划骨架结果集合进行归并与整理的最小协作契约。
/// </summary>
public interface IPlanningResultCoordinator
{
    /// <summary>
    /// 协调一组计划骨架结果。
    /// </summary>
    /// <param name="results">待协调的计划骨架结果集合。</param>
    /// <returns>协调后的计划骨架结果集合。</returns>
    IReadOnlyCollection<SchedulingPlanResult> Coordinate(
        IReadOnlyCollection<SchedulingPlanResult> results);
}
