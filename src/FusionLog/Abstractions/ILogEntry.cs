using FusionLog.Categories;
using FusionLog.Context;
using FusionLog.Entries;
using FusionLog.Levels;

namespace FusionLog.Abstractions;

/// <summary>
/// 定义日志条目的最小语义边界。
/// </summary>
public interface ILogEntry
{
    /// <summary>
    /// 获取日志时间。
    /// </summary>
    DateTimeOffset Timestamp { get; }

    /// <summary>
    /// 获取日志级别。
    /// </summary>
    LogLevel Level { get; }

    /// <summary>
    /// 获取日志分类。
    /// </summary>
    LogCategory Category { get; }

    /// <summary>
    /// 获取日志消息。
    /// </summary>
    LogMessage Message { get; }

    /// <summary>
    /// 获取日志上下文。
    /// </summary>
    LogContext Context { get; }

    /// <summary>
    /// 获取日志事件标识。
    /// </summary>
    LogEventId? EventId { get; }

    /// <summary>
    /// 获取异常摘要。
    /// </summary>
    LogExceptionInfo? Exception { get; }

    /// <summary>
    /// 获取附加字段。
    /// </summary>
    IReadOnlyCollection<LogProperty> Properties { get; }
}
