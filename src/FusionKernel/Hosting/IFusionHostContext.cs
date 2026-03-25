namespace FusionKernel.Hosting;

/// <summary>
/// 定义宿主上下文的最小只读边界。
/// </summary>
public interface IFusionHostContext
{
    /// <summary>
    /// 获取宿主标识。
    /// </summary>
    string HostId { get; }

    /// <summary>
    /// 获取宿主名称。
    /// </summary>
    string HostName { get; }

    /// <summary>
    /// 获取宿主运行模式。
    /// </summary>
    HostRunMode RunMode { get; }

    /// <summary>
    /// 获取运行时根路径。
    /// </summary>
    string RuntimeRoot { get; }
}
