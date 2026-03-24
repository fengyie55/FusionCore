using FusionDomain.ValueObjects;
using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.OrchestrationRequests;

/// <summary>
/// 表示调度侧需要评估作业推进状态的请求。
/// </summary>
public sealed record JobProgressEvaluationRequest(
    string RequestId,
    OrchestrationPriority Priority,
    OrchestrationRequestSource Source,
    DateTimeOffset RequestedAtUtc,
    ControlJobId? ControlJobId,
    ProcessJobId? ProcessJobId,
    RecipeId? RecipeId,
    string ReasonCode,
    JobContextRefreshRequest RefreshRequest)
    : SchedulerOrchestrationRequest(
        RequestId,
        OrchestrationIntentType.JobProgressEvaluation,
        Priority,
        Source,
        RequestedAtUtc);
