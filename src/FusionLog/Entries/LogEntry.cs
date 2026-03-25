using FusionLog.Abstractions;
using FusionLog.Categories;
using FusionLog.Context;
using FusionLog.Levels;

namespace FusionLog.Entries;

/// <summary>
/// 表示日志条目的最小默认实现。
/// </summary>
/// <param name="Timestamp">日志时间。</param>
/// <param name="Level">日志级别。</param>
/// <param name="Category">日志分类。</param>
/// <param name="Message">日志消息。</param>
/// <param name="Context">日志上下文。</param>
/// <param name="EventId">日志事件标识。</param>
/// <param name="Exception">异常摘要。</param>
/// <param name="Properties">附加字段集合。</param>
public sealed record LogEntry(
    DateTimeOffset Timestamp,
    LogLevel Level,
    LogCategory Category,
    LogMessage Message,
    LogContext Context,
    LogEventId? EventId,
    LogExceptionInfo? Exception,
    IReadOnlyCollection<LogProperty> Properties) : ILogEntry;
