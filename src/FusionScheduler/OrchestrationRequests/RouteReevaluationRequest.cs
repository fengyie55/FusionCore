using FusionDomain.ValueObjects;
using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.OrchestrationRequests;

/// <summary>
/// 表示调度侧需要重新评估路径相关编排的请求。
/// </summary>
public sealed record RouteReevaluationRequest(
    string RequestId,
    OrchestrationPriority Priority,
    OrchestrationRequestSource Source,
    DateTimeOffset RequestedAtUtc,
    ProcessJobId? ProcessJobId,
    MaterialId? MaterialId,
    EquipmentId? EquipmentId,
    string ReasonCode,
    RouteRefreshRequest RefreshRequest)
    : SchedulerOrchestrationRequest(
        RequestId,
        OrchestrationIntentType.RouteReevaluation,
        Priority,
        Source,
        RequestedAtUtc);
