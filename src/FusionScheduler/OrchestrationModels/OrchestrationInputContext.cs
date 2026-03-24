using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.OrchestrationModels;

/// <summary>
/// 表示生成内部编排请求时使用的最小输入上下文。
/// </summary>
public sealed record OrchestrationInputContext(
    OrchestrationRequestSource Source,
    DateTimeOffset RequestedAtUtc,
    string? CorrelationId);
