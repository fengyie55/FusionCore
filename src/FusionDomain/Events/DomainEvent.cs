namespace FusionDomain.Events;

/// <summary>
/// 定义领域事件的基础契约。
/// </summary>
public abstract record DomainEvent
{
    /// <summary>
    /// 获取事件实例创建时的 UTC 时间戳。
    /// </summary>
    public DateTimeOffset OccurredAtUtc { get; init; } = DateTimeOffset.UtcNow;
}
