using FusionScheduler.CoordinationIntents;

namespace FusionScheduler.CoordinationInputs;

/// <summary>
/// 表示执行前协调输入的最小公共基类。
/// </summary>
public abstract record ExecutionCoordinationRequest(
    string CoordinationRequestId,
    string PlanResultId,
    CoordinationIntentType IntentType,
    CoordinationPriority Priority,
    DateTimeOffset RequestedAtUtc);
