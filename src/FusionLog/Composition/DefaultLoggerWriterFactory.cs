using FusionLog.Abstractions;
using FusionLog.Context;
using FusionLog.Options;
using FusionLog.Writers;

namespace FusionLog.Composition;

/// <summary>
/// 提供默认日志写入器装配能力。
/// </summary>
public sealed class DefaultLoggerWriterFactory
{
    private readonly LogFilePathResolver _pathResolver = new();

    /// <summary>
    /// 按配置创建默认写入器。
    /// </summary>
    /// <param name="options">写入选项。</param>
    /// <param name="context">日志上下文。</param>
    /// <returns>默认写入器。</returns>
    public DefaultLoggerWriter Create(LoggingWriterOptions options, LogContext? context = null)
    {
        return new DefaultLoggerWriter(CreateInnerWriter(options, context));
    }

    /// <summary>
    /// 描述当前装配结果。
    /// </summary>
    /// <param name="options">写入选项。</param>
    /// <param name="context">日志上下文。</param>
    /// <returns>运行装配描述。</returns>
    public LoggingRuntimeDescriptor Describe(LoggingWriterOptions options, LogContext? context = null)
    {
        ArgumentNullException.ThrowIfNull(options);

        var filePath = options.File.Enabled
            ? _pathResolver.Resolve(options.File.WriteOptions, context)
            : null;

        return new LoggingRuntimeDescriptor(
            options.File.Enabled,
            options.Memory.Enabled,
            !options.Enabled || options.UseNullWriterWhenDisabled,
            filePath);
    }

    private ILoggerWriter CreateInnerWriter(LoggingWriterOptions options, LogContext? context)
    {
        ArgumentNullException.ThrowIfNull(options);

        var builder = new LoggingCompositionBuilder();

        if (!options.Enabled)
        {
            return options.UseNullWriterWhenDisabled ? new NullLoggerWriter() : builder.Build();
        }

        if (options.Memory.Enabled)
        {
            builder.Add(new MemoryLoggerWriter());
        }

        if (options.File.Enabled)
        {
            _ = context;
            builder.Add(new FileLoggerWriter(options.File.WriteOptions, _pathResolver));
        }

        if (options.UseNullWriterWhenDisabled && !options.File.Enabled && !options.Memory.Enabled)
        {
            builder.Add(new NullLoggerWriter());
        }

        return builder.Build();
    }
}
