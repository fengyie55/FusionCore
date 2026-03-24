using FusionScheduler.PlanningIntents;

namespace FusionScheduler.PlanningModels;

/// <summary>
/// 表示计划骨架生成阶段使用的最小输入上下文。
/// </summary>
public sealed record PlanningInputContext(
    string? CorrelationId,
    DateTimeOffset PlannedAtUtc,
    string? ContextVersion,
    PlanningPriority Priority);
