using FusionScheduler.EvaluationIntents;
using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.EvaluationModels;

/// <summary>
/// 表示调度评估边界使用的最小输入上下文。
/// </summary>
public sealed record EvaluationInputContext(
    string? CorrelationId,
    OrchestrationRequestSource Source,
    DateTimeOffset EvaluatedAtUtc,
    string? ContextVersion,
    EvaluationPriority Priority);
