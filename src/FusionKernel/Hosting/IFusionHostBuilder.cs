using FusionKernel.Context;

namespace FusionKernel.Hosting;

/// <summary>
/// 定义宿主构建器的最小边界。
/// </summary>
public interface IFusionHostBuilder
{
    /// <summary>
    /// 配置运行上下文。
    /// </summary>
    /// <param name="runtimeContext">运行上下文。</param>
    /// <returns>当前构建器。</returns>
    IFusionHostBuilder UseRuntimeContext(RuntimeContext runtimeContext);

    /// <summary>
    /// 配置宿主上下文。
    /// </summary>
    /// <param name="hostContext">宿主上下文。</param>
    /// <returns>当前构建器。</returns>
    IFusionHostBuilder UseHostContext(IFusionHostContext hostContext);

    /// <summary>
    /// 构建宿主实例。
    /// </summary>
    /// <returns>宿主实例。</returns>
    IFusionHost Build();
}
