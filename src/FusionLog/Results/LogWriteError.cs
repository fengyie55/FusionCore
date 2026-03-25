namespace FusionLog.Results;

/// <summary>
/// 表示日志写入失败的最小错误信息。
/// </summary>
/// <param name="Code">错误代码。</param>
/// <param name="Message">错误消息。</param>
/// <param name="WriterName">写入器名称。</param>
public sealed record LogWriteError(
    string Code,
    string Message,
    string? WriterName = null);
