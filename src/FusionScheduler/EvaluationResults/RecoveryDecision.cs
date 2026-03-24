using FusionDomain.ValueObjects;
using FusionScheduler.EvaluationIntents;
using FusionScheduler.Recovery;

namespace FusionScheduler.EvaluationResults;

/// <summary>
/// 表示恢复相关评估得出的最小内部结论。
/// </summary>
public sealed record RecoveryDecision(
    ProcessJobId? ProcessJobId,
    MaterialId? MaterialId,
    AlarmId? AlarmId,
    EquipmentId? EquipmentId,
    RecoveryReason? SuggestedReason,
    EvaluationConclusionKind ConclusionKind,
    string DecisionCode);
