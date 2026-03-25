using FusionKernel.Abstractions;
using FusionKernel.Lifecycle;
using FusionKernel.Results;

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
    /// 获取宿主初始化结果。
    /// </summary>
    HostInitializationResult InitializeHost();
}
