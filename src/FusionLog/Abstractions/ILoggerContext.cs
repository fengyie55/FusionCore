namespace FusionLog.Abstractions;

/// <summary>
/// 定义日志上下文的最小边界。
/// </summary>
public interface ILoggerContext
{
    /// <summary>
    /// 获取宿主标识。
    /// </summary>
    string? HostId { get; }

    /// <summary>
    /// 获取进程标识。
    /// </summary>
    string? ProcessId { get; }

    /// <summary>
    /// 获取模块标识。
    /// </summary>
    string? ModuleId { get; }

    /// <summary>
    /// 获取实例标识。
    /// </summary>
    string? InstanceId { get; }
}
