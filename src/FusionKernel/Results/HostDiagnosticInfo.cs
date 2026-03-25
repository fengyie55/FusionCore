using FusionKernel.Composition;
using FusionKernel.Hosting;
using FusionKernel.Modules;
using FusionKernel.Runtime;

namespace FusionKernel.Results;

/// <summary>
/// 表示宿主的最小诊断信息。
/// </summary>
public sealed record HostDiagnosticInfo(
    HostDescriptor Host,
    RuntimeDescriptor Runtime,
    HostState State,
    HostInitializationState InitializationState,
    IReadOnlyCollection<HostDependencyDescriptor> Dependencies,
    ModuleCollectionSnapshot Modules);
