using FusionLog.Composition;
using FusionLog.Context;
using FusionLog.Options;
using FusionLog.Writers;

namespace FusionLog;

/// <summary>
/// 提供 FusionLog 的最小边界入口。
/// </summary>
public static class LogBoundary
{
    /// <summary>
    /// 创建默认日志写入工厂。
    /// </summary>
    /// <returns>日志写入工厂。</returns>
    public static DefaultLoggerWriterFactory CreateFactory()
    {
        return new DefaultLoggerWriterFactory();
    }

    /// <summary>
    /// 根据最小写入选项创建默认日志写入器。
    /// </summary>
    /// <param name="options">写入选项。</param>
    /// <param name="context">日志上下文。</param>
    /// <returns>默认日志写入器。</returns>
    public static DefaultLoggerWriter CreateDefaultWriter(
        LoggingWriterOptions options,
        LogContext? context = null)
    {
        return CreateFactory().Create(options, context);
    }
}
