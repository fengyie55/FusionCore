namespace FusionScheduler.EventModels;

/// <summary>
/// 表示调度侧消费领域事件后产生的上下文更新意图集合。
/// </summary>
public sealed record SchedulerContextUpdateResult(
    IReadOnlyCollection<RouteRefreshRequest> RouteRefreshRequests,
    IReadOnlyCollection<TrackingRefreshRequest> TrackingRefreshRequests,
    IReadOnlyCollection<RecoveryEvaluationRequest> RecoveryEvaluationRequests,
    IReadOnlyCollection<JobContextRefreshRequest> JobContextRefreshRequests)
{
    /// <summary>
    /// 表示没有任何刷新意图的空结果。
    /// </summary>
    public static SchedulerContextUpdateResult Empty { get; } = new(
        Array.Empty<RouteRefreshRequest>(),
        Array.Empty<TrackingRefreshRequest>(),
        Array.Empty<RecoveryEvaluationRequest>(),
        Array.Empty<JobContextRefreshRequest>());
}
