namespace FusionKernel.Hosting;

/// <summary>
/// 表示宿主初始化状态。
/// </summary>
public enum HostInitializationState
{
    NotInitialized = 0,
    Initializing = 1,
    Initialized = 2,
    Failed = 3
}
