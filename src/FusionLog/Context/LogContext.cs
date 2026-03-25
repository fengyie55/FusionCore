using FusionLog.Abstractions;

namespace FusionLog.Context;

/// <summary>
/// 表示日志上下文的最小默认实现。
/// </summary>
/// <param name="Host">宿主上下文。</param>
/// <param name="Process">进程上下文。</param>
/// <param name="Module">模块上下文。</param>
public sealed record LogContext(
    HostLogContext? Host,
    ProcessLogContext? Process,
    ModuleLogContext? Module) : ILoggerContext
{
    /// <summary>
    /// 获取宿主标识。
    /// </summary>
    public string? HostId => Host?.HostId;

    /// <summary>
    /// 获取进程标识。
    /// </summary>
    public string? ProcessId => Process?.ProcessId;

    /// <summary>
    /// 获取模块标识。
    /// </summary>
    public string? ModuleId => Module?.ModuleId;

    /// <summary>
    /// 获取实例标识。
    /// </summary>
    public string? InstanceId => Module?.InstanceId;
}
