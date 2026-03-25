using FusionKernel.Hosting;
using FusionKernel.Runtime;
using FusionKernel.Services;

namespace FusionKernel.Modules;

/// <summary>
/// 表示模块初始化上下文。
/// </summary>
public sealed record ModuleInitializationContext(
    HostDescriptor Host,
    RuntimeContext Runtime,
    IServiceRegistrar ServiceRegistrar);
