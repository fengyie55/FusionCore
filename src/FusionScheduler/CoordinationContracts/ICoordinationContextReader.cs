using FusionScheduler.CoordinationInputs;
using FusionScheduler.PlanningResults;

namespace FusionScheduler.CoordinationContracts;

/// <summary>
/// 定义执行前协调阶段使用的最小只读上下文访问边界。
/// </summary>
public interface ICoordinationContextReader
{
    /// <summary>
    /// 基于计划结果读取执行前协调输入。
    /// </summary>
    /// <param name="planResult">计划骨架结果。</param>
    /// <returns>执行前协调输入。</returns>
    ExecutionCoordinationRequest ReadFor(SchedulingPlanResult planResult);
}
