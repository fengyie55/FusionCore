using FusionScheduler.CoordinationIntents;

namespace FusionScheduler.CoordinationInputs;

/// <summary>
/// 表示执行前协调阶段使用的最小输入上下文。
/// </summary>
public sealed record CoordinationInputContext(
    string? CorrelationId,
    DateTimeOffset CoordinatedAtUtc,
    string? ContextVersion,
    CoordinationPriority Priority);
