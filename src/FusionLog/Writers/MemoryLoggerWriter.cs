using FusionLog.Abstractions;
using FusionLog.Entries;
using FusionLog.Results;

namespace FusionLog.Writers;

/// <summary>
/// 提供最小内存日志写入实现。
/// </summary>
public sealed class MemoryLoggerWriter : ILoggerWriter
{
    private readonly List<LogEntry> _entries = new();

    /// <summary>
    /// 获取已写入日志集合。
    /// </summary>
    public IReadOnlyCollection<LogEntry> Entries => _entries;

    /// <summary>
    /// 写入日志条目。
    /// </summary>
    /// <param name="entry">日志条目。</param>
    /// <returns>写入结果。</returns>
    public LogWriteResult Write(ILogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        var validation = Validate(entry);
        if (!validation.Succeeded)
        {
            return new LogWriteResult(
                false,
                validation,
                new LogWriteError("LOG_VALIDATION_FAILED", "日志条目未通过最小校验。", nameof(MemoryLoggerWriter)),
                1,
                0);
        }

        _entries.Add(ToLogEntry(entry));
        return LogWriteResult.Success(validation);
    }

    private static LogValidationResult Validate(ILogEntry entry)
    {
        var issues = new List<LogValidationIssue>();

        if (string.IsNullOrWhiteSpace(entry.Message.Text))
        {
            issues.Add(new LogValidationIssue("LOG_MESSAGE_REQUIRED", "日志消息不能为空。"));
        }

        if (string.IsNullOrWhiteSpace(entry.Category.Name))
        {
            issues.Add(new LogValidationIssue("LOG_CATEGORY_REQUIRED", "日志分类不能为空。"));
        }

        return new LogValidationResult(issues);
    }

    private static LogEntry ToLogEntry(ILogEntry entry)
    {
        return entry as LogEntry
               ?? new LogEntry(
                   entry.Timestamp,
                   entry.Level,
                   entry.Category,
                   entry.Message,
                   entry.Context,
                   entry.EventId,
                   entry.Exception,
                   entry.Properties);
    }
}
