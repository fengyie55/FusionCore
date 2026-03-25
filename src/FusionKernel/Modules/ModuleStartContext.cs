using FusionKernel.Hosting;
using FusionKernel.Runtime;
using FusionKernel.Services;

namespace FusionKernel.Modules;

/// <summary>
/// 表示模块启动上下文。
/// </summary>
public sealed record ModuleStartContext(
    HostDescriptor Host,
    RuntimeContext Runtime,
    IServiceResolver ServiceResolver);
