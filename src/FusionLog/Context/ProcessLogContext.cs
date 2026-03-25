namespace FusionLog.Context;

/// <summary>
/// 表示进程级日志上下文。
/// </summary>
/// <param name="ProcessId">进程标识。</param>
/// <param name="ProcessName">进程名称。</param>
public sealed record ProcessLogContext(
    string? ProcessId,
    string? ProcessName);
