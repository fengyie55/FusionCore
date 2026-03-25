namespace FusionLog.Categories;

/// <summary>
/// 提供运行时相关日志分类集合。
/// </summary>
public static class RuntimeLogCategory
{
    /// <summary>
    /// 操作分类。
    /// </summary>
    public static LogCategory Operation { get; } = new(LogCategoryNames.Operation);

    /// <summary>
    /// 故障分类。
    /// </summary>
    public static LogCategory Fault { get; } = new(LogCategoryNames.Fault);

    /// <summary>
    /// 审计分类。
    /// </summary>
    public static LogCategory Audit { get; } = new(LogCategoryNames.Audit);
}
