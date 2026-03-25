using FusionLog.Abstractions;
using FusionLog.Results;

namespace FusionLog.Writers;

/// <summary>
/// 提供最小空日志写入实现。
/// </summary>
public sealed class NullLoggerWriter : ILoggerWriter
{
    /// <summary>
    /// 写入日志条目。
    /// </summary>
    /// <param name="entry">日志条目。</param>
    /// <returns>写入结果。</returns>
    public LogWriteResult Write(ILogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);
        return LogWriteResult.Success(LogValidationResult.Success());
    }
}
