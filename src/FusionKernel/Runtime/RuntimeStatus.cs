namespace FusionKernel.Runtime;

/// <summary>
/// 表示运行时状态。
/// </summary>
public enum RuntimeStatus
{
    Created = 0,
    Initializing = 1,
    Initialized = 2,
    Starting = 3,
    Running = 4,
    Stopping = 5,
    Stopped = 6,
    Failed = 7
}
