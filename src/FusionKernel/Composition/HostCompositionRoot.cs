namespace FusionKernel.Composition;

/// <summary>
/// 提供宿主组合的最小静态入口。
/// </summary>
public static class HostCompositionRoot
{
    /// <summary>
    /// 创建宿主构造器。
    /// </summary>
    public static HostRuntimeBuilder CreateBuilder(
        HostCompositionOptions? options = null,
        HostBootstrapContext? bootstrapContext = null)
    {
        return new HostRuntimeBuilder()
            .UseOptions(options ?? CreateDefaultOptions())
            .UseBootstrapContext(bootstrapContext ?? new HostBootstrapContext());
    }

    /// <summary>
    /// 创建默认宿主组合选项。
    /// </summary>
    public static HostCompositionOptions CreateDefaultOptions()
    {
        return new HostCompositionOptions(
            "FusionHost",
            "Fusion Host",
            "FusionRuntime",
            AppContext.BaseDirectory,
            Hosting.HostRunMode.Production,
            "Production");
    }
}
