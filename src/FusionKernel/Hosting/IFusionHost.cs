using FusionKernel.Abstractions;
using FusionKernel.Lifecycle;
using FusionKernel.Results;
using FusionKernel.Runtime;

namespace FusionKernel.Hosting;

/// <summary>
/// 定义 Fusion 宿主的最小边界。
/// </summary>
public interface IFusionHost : IFusionComponent, IInitializable, IAsyncStartable, IAsyncStoppable
{
    /// <summary>
    /// 获取宿主上下文。
    /// </summary>
    IFusionHostContext Context { get; }

    /// <summary>
    /// 获取宿主描述。
    /// </summary>
    HostDescriptor Descriptor { get; }

    /// <summary>
    /// 获取运行时上下文。
    /// </summary>
    RuntimeContext RuntimeContext { get; }

    /// <summary>
    /// 获取当前宿主状态。
    /// </summary>
    HostState State { get; }

    /// <summary>
    /// 获取初始化状态。
    /// </summary>
    HostInitializationState InitializationState { get; }

    /// <summary>
    /// 获取最小诊断信息。
    /// </summary>
    HostDiagnosticInfo DiagnosticInfo { get; }

    /// <summary>
    /// 执行宿主初始化。
    /// </summary>
    HostInitializationResult InitializeHost();

    /// <summary>
    /// 执行宿主启动。
    /// </summary>
    HostStartResult StartHost();

    /// <summary>
    /// 执行宿主停止。
    /// </summary>
    HostStopResult StopHost();
}
