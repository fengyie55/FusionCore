namespace FusionLog.Options;

/// <summary>
/// 表示文件日志写入配置。
/// </summary>
/// <param name="Enabled">是否启用文件写入。</param>
/// <param name="WriteOptions">文件写入选项。</param>
public sealed record FileLoggingOptions(
    bool Enabled,
    FileLogWriteOptions WriteOptions);
