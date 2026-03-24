using FusionScheduler.OrchestrationRequests;

namespace FusionScheduler.OrchestrationContracts;

/// <summary>
/// 定义调度内部编排请求进入后续管道的最小网关边界。
/// </summary>
public interface ISchedulerOrchestrationGateway
{
    /// <summary>
    /// 提交待进入内部编排边界的请求集合。
    /// </summary>
    /// <param name="requests">待提交的编排请求集合。</param>
    void Submit(IReadOnlyCollection<SchedulerOrchestrationRequest> requests);
}
