using FusionLog.Results;

namespace FusionLog.Abstractions;

/// <summary>
/// 定义日志写入边界的最小契约。
/// </summary>
public interface ILoggerWriter
{
    /// <summary>
    /// 写入日志条目。
    /// </summary>
    /// <param name="entry">日志条目。</param>
    /// <returns>写入结果。</returns>
    LogWriteResult Write(ILogEntry entry);
}
