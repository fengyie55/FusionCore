using FusionScheduler.CoordinationIntents;

namespace FusionScheduler.CoordinationResults;

/// <summary>
/// 表示执行前协调结果的最小摘要。
/// </summary>
public sealed record CoordinationSummary(
    CoordinationOutcomeKind OutcomeKind,
    string SummaryCode,
    string? Notes);
