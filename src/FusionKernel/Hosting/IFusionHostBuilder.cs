using FusionKernel.Runtime;

namespace FusionKernel.Hosting;

/// <summary>
/// 定义宿主构建器的最小边界。
/// </summary>
public interface IFusionHostBuilder
{
    /// <summary>
    /// 配置运行上下文。
    /// </summary>
    IFusionHostBuilder UseRuntimeContext(RuntimeContext runtimeContext);

    /// <summary>
    /// 配置宿主上下文。
    /// </summary>
    IFusionHostBuilder UseHostContext(IFusionHostContext hostContext);

    /// <summary>
    /// 构建宿主实例。
    /// </summary>
    IFusionHost Build();
}
