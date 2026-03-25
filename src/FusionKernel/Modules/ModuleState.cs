namespace FusionKernel.Modules;

/// <summary>
/// 表示模块生命周期状态。
/// </summary>
public enum ModuleState
{
    Registered = 0,
    Initializing = 1,
    Initialized = 2,
    Starting = 3,
    Started = 4,
    Stopping = 5,
    Stopped = 6,
    Failed = 7
}
