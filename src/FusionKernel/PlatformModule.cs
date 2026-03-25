using FusionKernel.Modules;
using FusionKernel.Services;

namespace FusionKernel;

/// <summary>
/// 提供 FusionKernel 的最小占位模块实现。
/// </summary>
public sealed class PlatformModule : IFusionModule
{
    /// <summary>
    /// 获取模块标识。
    /// </summary>
    public string Id => "FusionKernel.PlatformModule";

    /// <summary>
    /// 获取模块名称。
    /// </summary>
    public string Name => nameof(PlatformModule);

    /// <summary>
    /// 获取模块描述。
    /// </summary>
    public IFusionModuleDescriptor Descriptor { get; } = new PlatformModuleDescriptor();

    /// <summary>
    /// 执行最小初始化。
    /// </summary>
    public void Initialize()
    {
    }

    /// <summary>
    /// 配置模块服务。
    /// </summary>
    /// <param name="registrar">服务注册器。</param>
    public void ConfigureServices(IServiceRegistrar registrar)
    {
    }

    private sealed class PlatformModuleDescriptor : IFusionModuleDescriptor
    {
        public string ModuleId => "FusionKernel.PlatformModule";

        public string ModuleName => "FusionKernel Platform Module";

        public string Version => "1.0.0";
    }
}
