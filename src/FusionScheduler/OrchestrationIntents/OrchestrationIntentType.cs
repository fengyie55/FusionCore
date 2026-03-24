namespace FusionScheduler.OrchestrationIntents;

/// <summary>
/// 表示调度内部编排请求的最小意图分类。
/// </summary>
public enum OrchestrationIntentType
{
    Unknown = 0,
    RouteReevaluation = 1,
    MaterialFlowReplan = 2,
    JobProgressEvaluation = 3,
    RecoveryAssessment = 4,
}
