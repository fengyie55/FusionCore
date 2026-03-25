using FusionKernel.Hosting;
using FusionKernel.Runtime;

namespace FusionKernel.Modules;

/// <summary>
/// 表示模块停止上下文。
/// </summary>
public sealed record ModuleStopContext(
    HostDescriptor Host,
    RuntimeContext Runtime);
