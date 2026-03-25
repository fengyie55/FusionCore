using FusionLog.Abstractions;
using FusionLog.Results;

namespace FusionLog.Writers;

/// <summary>
/// 提供日志写入边界的最小默认实现。
/// </summary>
public sealed class DefaultLoggerWriter : ILoggerWriter
{
    private readonly ILoggerWriter _innerWriter;

    /// <summary>
    /// 创建默认日志写入器。
    /// </summary>
    /// <param name="innerWriter">内部写入器。</param>
    public DefaultLoggerWriter(ILoggerWriter? innerWriter = null)
    {
        _innerWriter = innerWriter ?? new NullLoggerWriter();
    }

    /// <summary>
    /// 获取内部写入器。
    /// </summary>
    public ILoggerWriter InnerWriter => _innerWriter;

    /// <summary>
    /// 写入日志条目。
    /// </summary>
    /// <param name="entry">日志条目。</param>
    /// <returns>写入结果。</returns>
    public LogWriteResult Write(ILogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);
        return _innerWriter.Write(entry);
    }
}
