using FusionScheduler.CoordinationInputs;
using FusionScheduler.CoordinationResults;

namespace FusionScheduler.CoordinationContracts;

/// <summary>
/// 定义执行前协调边界的最小服务契约。
/// </summary>
public interface IPlanCoordinationService
{
    /// <summary>
    /// 对待协调输入集合执行执行前协调。
    /// </summary>
    /// <param name="requests">待协调输入集合。</param>
    /// <param name="inputContext">协调输入上下文。</param>
    /// <returns>协调结果集合。</returns>
    IReadOnlyCollection<CoordinationResult> Coordinate(
        IReadOnlyCollection<ExecutionCoordinationRequest> requests,
        CoordinationInputContext inputContext);
}
