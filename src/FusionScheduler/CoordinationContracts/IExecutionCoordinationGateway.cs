using FusionScheduler.CoordinationInputs;

namespace FusionScheduler.CoordinationContracts;

/// <summary>
/// 定义计划骨架进入执行前协调边界的最小网关契约。
/// </summary>
public interface IExecutionCoordinationGateway
{
    /// <summary>
    /// 提交待协调的输入集合。
    /// </summary>
    /// <param name="requests">待协调输入集合。</param>
    void Submit(IReadOnlyCollection<ExecutionCoordinationRequest> requests);
}
