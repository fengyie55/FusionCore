using FusionScheduler.PlanningIntents;

namespace FusionScheduler.PlanningResults;

/// <summary>
/// 表示计划骨架生成结果的最小摘要。
/// </summary>
public sealed record PlanningSummary(
    PlanningConclusionKind ConclusionKind,
    string SummaryCode,
    string? Notes);
