namespace FusionKernel.Hosting;

/// <summary>
/// 表示宿主运行状态。
/// </summary>
public enum HostState
{
    Constructed = 0,
    Initializing = 1,
    Initialized = 2,
    Starting = 3,
    Started = 4,
    Stopping = 5,
    Stopped = 6,
    Failed = 7
}
