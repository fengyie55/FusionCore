namespace FusionScheduler.OrchestrationIntents;

/// <summary>
/// 表示编排请求的来源边界。
/// </summary>
public enum OrchestrationRequestSource
{
    Unknown = 0,
    DomainEventConsumption = 1,
    ContextRefresh = 2,
}
