using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.EvaluationIntents;

/// <summary>
/// 表示调度评估边界处理的最小意图分类。
/// </summary>
public enum EvaluationIntentType
{
    Unknown = 0,
    Route = 1,
    MaterialFlow = 2,
    JobProgress = 3,
    Recovery = 4,
}

/// <summary>
/// 提供编排意图到评估意图的最小映射。
/// </summary>
public static class EvaluationIntentTypeExtensions
{
    /// <summary>
    /// 将编排意图转换为评估意图。
    /// </summary>
    /// <param name="intentType">编排意图。</param>
    /// <returns>评估意图。</returns>
    public static EvaluationIntentType ToEvaluationIntentType(this OrchestrationIntentType intentType)
    {
        return intentType switch
        {
            OrchestrationIntentType.RouteReevaluation => EvaluationIntentType.Route,
            OrchestrationIntentType.MaterialFlowReplan => EvaluationIntentType.MaterialFlow,
            OrchestrationIntentType.JobProgressEvaluation => EvaluationIntentType.JobProgress,
            OrchestrationIntentType.RecoveryAssessment => EvaluationIntentType.Recovery,
            _ => EvaluationIntentType.Unknown,
        };
    }
}
