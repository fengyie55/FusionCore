using FusionLog.Abstractions;
using FusionLog.Writers;

namespace FusionLog.Composition;

/// <summary>
/// 提供日志写入组合构建器。
/// </summary>
public sealed class LoggingCompositionBuilder
{
    private readonly List<ILoggerWriter> _writers = new();

    /// <summary>
    /// 添加写入器。
    /// </summary>
    /// <param name="writer">写入器。</param>
    /// <returns>当前构建器。</returns>
    public LoggingCompositionBuilder Add(ILoggerWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
        _writers.Add(writer);
        return this;
    }

    /// <summary>
    /// 构建最终写入器。
    /// </summary>
    /// <returns>日志写入器。</returns>
    public ILoggerWriter Build()
    {
        if (_writers.Count == 0)
        {
            return new NullLoggerWriter();
        }

        if (_writers.Count == 1)
        {
            return _writers[0];
        }

        return new CompositeLoggerWriter(_writers);
    }
}
