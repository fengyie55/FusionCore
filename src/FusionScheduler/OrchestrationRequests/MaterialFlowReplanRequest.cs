using FusionDomain.ValueObjects;
using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.OrchestrationRequests;

/// <summary>
/// 表示调度侧需要重新规划物料流转的请求。
/// </summary>
public sealed record MaterialFlowReplanRequest(
    string RequestId,
    OrchestrationPriority Priority,
    OrchestrationRequestSource Source,
    DateTimeOffset RequestedAtUtc,
    MaterialId? MaterialId,
    CarrierId? CarrierId,
    SubstrateId? SubstrateId,
    ModuleId? ModuleId,
    string? LocationCode,
    TrackingRefreshRequest RefreshRequest)
    : SchedulerOrchestrationRequest(
        RequestId,
        OrchestrationIntentType.MaterialFlowReplan,
        Priority,
        Source,
        RequestedAtUtc);
