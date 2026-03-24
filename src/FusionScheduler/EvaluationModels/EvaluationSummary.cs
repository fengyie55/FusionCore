using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.EvaluationModels;

/// <summary>
/// 表示调度评估结论的最小摘要。
/// </summary>
public sealed record EvaluationSummary(
    EvaluationConclusionKind ConclusionKind,
    string SummaryCode,
    string? Notes);
