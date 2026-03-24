using FusionDomain.ValueObjects;
using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationIntents;
using FusionScheduler.Recovery;

namespace FusionScheduler.OrchestrationRequests;

/// <summary>
/// 表示调度侧需要重新评估恢复编排的请求。
/// </summary>
public sealed record RecoveryAssessmentRequest(
    string RequestId,
    OrchestrationPriority Priority,
    OrchestrationRequestSource Source,
    DateTimeOffset RequestedAtUtc,
    ProcessJobId? ProcessJobId,
    MaterialId? MaterialId,
    AlarmId? AlarmId,
    EquipmentId? EquipmentId,
    RecoveryReason? SuggestedReason,
    RecoveryEvaluationRequest RefreshRequest)
    : SchedulerOrchestrationRequest(
        RequestId,
        OrchestrationIntentType.RecoveryAssessment,
        Priority,
        Source,
        RequestedAtUtc);
