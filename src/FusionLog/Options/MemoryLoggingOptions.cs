namespace FusionLog.Options;

/// <summary>
/// 表示内存日志写入配置。
/// </summary>
/// <param name="Enabled">是否启用内存写入。</param>
public sealed record MemoryLoggingOptions(
    bool Enabled);
