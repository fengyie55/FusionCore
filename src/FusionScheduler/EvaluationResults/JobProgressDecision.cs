using FusionDomain.ValueObjects;
using FusionScheduler.EvaluationIntents;

namespace FusionScheduler.EvaluationResults;

/// <summary>
/// 表示作业推进相关评估得出的最小内部结论。
/// </summary>
public sealed record JobProgressDecision(
    ControlJobId? ControlJobId,
    ProcessJobId? ProcessJobId,
    RecipeId? RecipeId,
    EvaluationConclusionKind ConclusionKind,
    string DecisionCode);
