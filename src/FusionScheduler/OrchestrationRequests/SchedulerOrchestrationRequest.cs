using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.OrchestrationRequests;

/// <summary>
/// 表示调度内部编排请求的最小公共基类。
/// </summary>
public abstract record SchedulerOrchestrationRequest(
    string RequestId,
    OrchestrationIntentType IntentType,
    OrchestrationPriority Priority,
    OrchestrationRequestSource Source,
    DateTimeOffset RequestedAtUtc);
