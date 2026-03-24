using FusionDomain.ValueObjects;
using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.EvaluationResults;

/// <summary>
/// 表示物料流转相关评估得出的最小内部结论。
/// </summary>
public sealed record MaterialFlowDecision(
    MaterialId? MaterialId,
    CarrierId? CarrierId,
    SubstrateId? SubstrateId,
    ModuleId? ModuleId,
    EvaluationConclusionKind ConclusionKind,
    string DecisionCode);
