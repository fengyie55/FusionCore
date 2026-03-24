using FusionDomain.ValueObjects;
using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.EvaluationResults;

/// <summary>
/// 表示路径相关评估得出的最小内部结论。
/// </summary>
public sealed record RouteDecision(
    ProcessJobId? ProcessJobId,
    MaterialId? MaterialId,
    EquipmentId? EquipmentId,
    EvaluationConclusionKind ConclusionKind,
    string DecisionCode);
