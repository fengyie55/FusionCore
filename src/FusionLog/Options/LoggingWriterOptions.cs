namespace FusionLog.Options;

/// <summary>
/// 表示日志写入组合配置。
/// </summary>
/// <param name="Enabled">是否启用日志。</param>
/// <param name="File">文件写入配置。</param>
/// <param name="Memory">内存写入配置。</param>
/// <param name="UseNullWriterWhenDisabled">禁用时是否使用空写入器。</param>
public sealed record LoggingWriterOptions(
    bool Enabled,
    FileLoggingOptions File,
    MemoryLoggingOptions Memory,
    bool UseNullWriterWhenDisabled);
