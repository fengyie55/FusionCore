namespace FusionScheduler.EventContracts;

/// <summary>
/// 提供调度侧消费领域事件时的最小上下文信息。
/// </summary>
public interface ISchedulerEventConsumptionContext
{
    /// <summary>
    /// 获取消费动作发生的时间。
    /// </summary>
    DateTimeOffset ConsumedAtUtc { get; }

    /// <summary>
    /// 获取消费者名称。
    /// </summary>
    string ConsumerName { get; }

    /// <summary>
    /// 获取可选的来源边界提示。
    /// </summary>
    string? SourceBoundary { get; }
}
