using FusionKernel.Context;

namespace FusionKernel.Hosting;

/// <summary>
/// 提供宿主上下文的最小默认实现。
/// </summary>
public sealed record FusionHostContext(
    string HostId,
    string HostName,
    HostRunMode RunMode,
    string RuntimeRoot,
    RuntimeContext RuntimeContext) : IFusionHostContext;
