namespace FusionLog.Context;

/// <summary>
/// 表示宿主级日志上下文。
/// </summary>
/// <param name="HostId">宿主标识。</param>
/// <param name="HostName">宿主名称。</param>
public sealed record HostLogContext(
    string? HostId,
    string? HostName);
