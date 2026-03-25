namespace FusionKernel.Hosting;

/// <summary>
/// 表示宿主运行模式的最小分类。
/// </summary>
public enum HostRunMode
{
    Unknown = 0,
    Development = 1,
    Simulation = 2,
    Production = 3,
}
