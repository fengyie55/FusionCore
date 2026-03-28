using FusionKernel.Hosting;
using FusionKernel.Results;

namespace FusionApp.Runtime;

/// <summary>
/// 表示 FusionApp 的最小应用运行体。
/// </summary>
public sealed class ApplicationRuntime
{
    /// <summary>
    /// 获取应用运行摘要。
    /// </summary>
    public ApplicationRuntimeDescriptor Descriptor { get; }

    /// <summary>
    /// 获取底层宿主实例。
    /// </summary>
    public IFusionHost Host { get; }

    /// <summary>
    /// 创建应用运行体。
    /// </summary>
    public ApplicationRuntime(ApplicationRuntimeDescriptor descriptor, IFusionHost host)
    {
        Descriptor = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
        Host = host ?? throw new ArgumentNullException(nameof(host));
    }

    /// <summary>
    /// 执行宿主初始化。
    /// </summary>
    public HostInitializationResult Initialize()
    {
        return Host.InitializeHost();
    }

    /// <summary>
    /// 执行宿主启动。
    /// </summary>
    public HostStartResult Start()
    {
        return Host.StartHost();
    }

    /// <summary>
    /// 执行宿主停止。
    /// </summary>
    public HostStopResult Stop()
    {
        return Host.StopHost();
    }
}
