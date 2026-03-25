using FusionKernel.Hosting;

namespace FusionKernel.Runtime;

/// <summary>
/// 表示宿主最小运行上下文。
/// </summary>
public sealed record RuntimeContext(
    RuntimeInstanceId InstanceId,
    string RuntimeRoot,
    HostRunMode RunMode,
    string? Profile,
    RuntimeStatus Status = RuntimeStatus.Created,
    object? ConfigurationProvider = null,
    object? ConfigurationSnapshot = null,
    object? LoggerWriter = null,
    object? LoggerContext = null)
{
    /// <summary>
    /// 兼容旧的运行时标识文本访问方式。
    /// </summary>
    public string RuntimeId => InstanceId.Value;

    /// <summary>
    /// 获取运行时描述。
    /// </summary>
    public RuntimeDescriptor Descriptor => new(InstanceId, RuntimeRoot, RunMode, Profile);

    /// <summary>
    /// 获取运行环境描述。
    /// </summary>
    public RuntimeEnvironmentDescriptor Environment => new(Profile, RuntimeRoot);
}
