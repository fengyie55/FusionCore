using FusionLog.Abstractions;
using FusionLog.Results;

namespace FusionLog.Writers;

/// <summary>
/// 提供最小组合日志写入实现。
/// </summary>
public sealed class CompositeLoggerWriter : ILoggerWriter
{
    private readonly IReadOnlyCollection<ILoggerWriter> _writers;

    /// <summary>
    /// 创建组合日志写入器。
    /// </summary>
    /// <param name="writers">内部写入器集合。</param>
    public CompositeLoggerWriter(IReadOnlyCollection<ILoggerWriter> writers)
    {
        _writers = writers ?? Array.Empty<ILoggerWriter>();
    }

    /// <summary>
    /// 写入日志条目。
    /// </summary>
    /// <param name="entry">日志条目。</param>
    /// <returns>聚合写入结果。</returns>
    public LogWriteResult Write(ILogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        if (_writers.Count == 0)
        {
            return new LogWriteResult(true, LogValidationResult.Success(), null, 0, 0);
        }

        var issues = new List<LogValidationIssue>();
        var attempted = 0;
        var succeeded = 0;
        LogWriteError? firstError = null;

        foreach (var writer in _writers)
        {
            attempted++;
            var result = writer.Write(entry);
            issues.AddRange(result.ValidationResult.Issues);

            if (result.Succeeded)
            {
                succeeded++;
            }
            else if (firstError is null)
            {
                firstError = result.Error;
            }
        }

        return new LogWriteResult(
            succeeded == attempted,
            new LogValidationResult(issues),
            firstError,
            attempted,
            succeeded);
    }
}
