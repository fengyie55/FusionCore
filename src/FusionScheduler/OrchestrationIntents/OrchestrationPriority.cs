namespace FusionScheduler.OrchestrationIntents;

/// <summary>
/// 表示调度内部编排请求的最小优先级语义。
/// </summary>
public enum OrchestrationPriority
{
    Normal = 0,
    High = 1,
    Critical = 2,
}
