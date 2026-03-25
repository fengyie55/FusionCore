using FusionLog.Categories;

namespace FusionLog;

/// <summary>
/// 表示兼容性日志通道模型。
/// </summary>
/// <param name="Name">通道名称。</param>
public sealed record LogChannel(string Name)
{
    /// <summary>
    /// 转换为日志分类。
    /// </summary>
    /// <returns>日志分类。</returns>
    public LogCategory ToCategory()
    {
        return new LogCategory(Name);
    }
}
