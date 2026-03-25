using System.Text;
using FusionLog.Abstractions;
using FusionLog.Context;
using FusionLog.Options;
using FusionLog.Results;

namespace FusionLog.Writers;

/// <summary>
/// 提供最小文件日志写入实现。
/// </summary>
public sealed class FileLoggerWriter : ILoggerWriter
{
    private readonly FileLogWriteOptions _options;
    private readonly LogFilePathResolver _pathResolver;

    /// <summary>
    /// 创建文件日志写入器。
    /// </summary>
    /// <param name="options">文件写入选项。</param>
    /// <param name="pathResolver">路径解析器。</param>
    public FileLoggerWriter(
        FileLogWriteOptions options,
        LogFilePathResolver? pathResolver = null)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _pathResolver = pathResolver ?? new LogFilePathResolver();
    }

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
                new LogWriteError("LOG_VALIDATION_FAILED", "日志条目未通过最小校验。", nameof(FileLoggerWriter)),
                1,
                0);
        }

        try
        {
            var descriptor = _pathResolver.Resolve(_options, entry.Context);
            Directory.CreateDirectory(descriptor.DirectoryPath);
            File.AppendAllText(descriptor.FilePath, FormatLine(entry), Encoding.UTF8);
            return LogWriteResult.Success(validation);
        }
        catch (Exception exception)
        {
            return new LogWriteResult(
                false,
                validation,
                new LogWriteError("FILE_WRITE_FAILED", exception.Message, nameof(FileLoggerWriter)),
                1,
                0);
        }
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

    private static string FormatLine(ILogEntry entry)
    {
        var exceptionText = entry.Exception is null
            ? string.Empty
            : $" | EX={entry.Exception.ExceptionType}:{entry.Exception.Message}";

        return $"{entry.Timestamp:O} | {entry.Level} | {entry.Category.Name} | HOST={entry.Context.HostId ?? "-"} | PROC={entry.Context.ProcessId ?? "-"} | MOD={entry.Context.ModuleId ?? "-"} | {entry.Message.Text}{exceptionText}{Environment.NewLine}";
    }
}
