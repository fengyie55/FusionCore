namespace FusionLog.Entries;

/// <summary>
/// 表示日志事件标识。
/// </summary>
/// <param name="Id">事件标识。</param>
/// <param name="Name">事件名称。</param>
public sealed record LogEventId(
    int Id,
    string? Name);
