using FusionKernel.Hosting;

namespace FusionKernel.Runtime;

/// <summary>
/// 描述运行时的最小身份信息。
/// </summary>
public sealed record RuntimeDescriptor(
    RuntimeInstanceId InstanceId,
    string RuntimeRoot,
    HostRunMode RunMode,
    string? Profile);
