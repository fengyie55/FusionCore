using FusionScheduler.CoordinationIntents;

namespace FusionScheduler.CoordinationResults;

/// <summary>
/// 表示执行前协调边界输出的最小结果对象。
/// </summary>
public sealed record CoordinationResult(
    string CoordinationResultId,
    string CoordinationRequestId,
    CoordinationIntentType IntentType,
    CoordinationPriority Priority,
    CoordinationBasisReference BasisReference,
    CoordinationSummary Summary,
    CoordinationDecision Decision,
    IReadOnlyCollection<CoordinationConflict> Conflicts,
    IReadOnlyCollection<CoordinationPrecheck> Prechecks);
