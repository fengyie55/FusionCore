using FusionKernel.Hosting;

namespace FusionKernel.Context;

/// <summary>
/// 表示平台运行时上下文的最小模型。
/// </summary>
public sealed record RuntimeContext(
    string RuntimeId,
    string RuntimeRoot,
    HostRunMode RunMode,
    string? EnvironmentName);
