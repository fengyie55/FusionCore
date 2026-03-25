namespace FusionKernel.Lifecycle;

/// <summary>
/// 表示平台底座生命周期阶段的最小状态集合。
/// </summary>
public enum LifecycleState
{
    Created = 0,
    Initialized = 1,
    Starting = 2,
    Started = 3,
    Stopping = 4,
    Stopped = 5,
}
