using FusionScheduler.CoordinationIntents;

namespace FusionScheduler.CoordinationResults;

/// <summary>
/// 表示执行前协调阶段得出的最小结论。
/// </summary>
public sealed record CoordinationDecision(
    CoordinationOutcomeKind OutcomeKind,
    string DecisionCode,
    bool CanProceedToExecutionPreparation);
