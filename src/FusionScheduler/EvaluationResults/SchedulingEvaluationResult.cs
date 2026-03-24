using FusionScheduler.EvaluationIntents;
using FusionScheduler.EvaluationModels;

namespace FusionScheduler.EvaluationResults;

/// <summary>
/// 表示调度评估边界输出的最小结果对象。
/// </summary>
public sealed record SchedulingEvaluationResult(
    string ResultId,
    string RequestId,
    EvaluationIntentType IntentType,
    EvaluationPriority Priority,
    EvaluationSummary Summary,
    RouteDecision? RouteDecision,
    MaterialFlowDecision? MaterialFlowDecision,
    JobProgressDecision? JobProgressDecision,
    RecoveryDecision? RecoveryDecision);
