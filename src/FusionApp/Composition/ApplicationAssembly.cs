using FusionApp.Runtime;
using FusionKernel.Composition;

namespace FusionApp.Composition;

/// <summary>
/// 表示 FusionApp 的完整装配结果。
/// </summary>
public sealed record ApplicationAssembly(
    ApplicationOptions Options,
    ApplicationBoundary Boundary,
    ApplicationBootstrapContext BootstrapContext,
    ApplicationRuntimeDescriptor RuntimeDescriptor,
    ApplicationUiBootstrapDescriptor UiBootstrapDescriptor,
    ApplicationRuntime Runtime)
{
    /// <summary>
    /// 获取宿主启动上下文。
    /// </summary>
    public HostBootstrapContext HostBootstrapContext => BootstrapContext.HostBootstrapContext;
}
