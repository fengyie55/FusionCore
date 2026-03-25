namespace FusionLog.Categories;

/// <summary>
/// 提供进程级日志分类集合。
/// </summary>
public static class ProcessLogCategory
{
    /// <summary>
    /// 运行时分类。
    /// </summary>
    public static LogCategory Runtime { get; } = new(LogCategoryNames.Runtime);

    /// <summary>
    /// IPC 分类。
    /// </summary>
    public static LogCategory Ipc { get; } = new(LogCategoryNames.Ipc);

    /// <summary>
    /// 性能分类。
    /// </summary>
    public static LogCategory Performance { get; } = new(LogCategoryNames.Performance);
}
