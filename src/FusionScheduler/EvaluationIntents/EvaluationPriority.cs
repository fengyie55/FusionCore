using FusionScheduler.OrchestrationIntents;

namespace FusionScheduler.EvaluationIntents;

/// <summary>
/// 表示调度评估的最小优先级语义。
/// </summary>
public enum EvaluationPriority
{
    Normal = 0,
    High = 1,
    Critical = 2,
}

/// <summary>
/// 提供编排优先级到评估优先级的最小映射。
/// </summary>
public static class EvaluationPriorityExtensions
{
    /// <summary>
    /// 将编排优先级转换为评估优先级。
    /// </summary>
    /// <param name="priority">编排优先级。</param>
    /// <returns>评估优先级。</returns>
    public static EvaluationPriority ToEvaluationPriority(this OrchestrationPriority priority)
    {
        return priority switch
        {
            OrchestrationPriority.High => EvaluationPriority.High,
            OrchestrationPriority.Critical => EvaluationPriority.Critical,
            _ => EvaluationPriority.Normal,
        };
    }
}
