using FusionKernel.Abstractions;
using FusionKernel.Lifecycle;
using FusionKernel.Services;

namespace FusionKernel.Modules;

/// <summary>
/// 定义 Fusion 模块的最小边界。
/// </summary>
public interface IFusionModule : IFusionComponent, IInitializable
{
    /// <summary>
    /// 获取模块描述信息。
    /// </summary>
    IFusionModuleDescriptor Descriptor { get; }

    /// <summary>
    /// 配置当前模块需要暴露的服务。
    /// </summary>
    /// <param name="registrar">服务注册器。</param>
    void ConfigureServices(IServiceRegistrar registrar);
}
