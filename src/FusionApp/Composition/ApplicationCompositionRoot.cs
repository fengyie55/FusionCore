using FusionConfig.Abstractions;
using FusionConfig.Runtime;
using FusionKernel.Composition;
using FusionKernel.Hosting;
using FusionKernel.Modules;
using FusionLog.Abstractions;
using FusionApp.Runtime;

namespace FusionApp.Composition;

/// <summary>
/// 提供 FusionApp 的最小应用装配入口。
/// </summary>
public static class ApplicationCompositionRoot
{
    /// <summary>
    /// 创建默认应用选项。
    /// </summary>
    public static ApplicationOptions CreateDefaultOptions(RuntimeRootOptions? runtimeRoot = null)
    {
        var resolvedRuntimeRoot = runtimeRoot ?? RuntimeRootOptions.CreateDefault();

        return new ApplicationOptions(
            "FusionApp",
            "FusionCore Application",
            "FusionHost",
            "Fusion Host",
            "FusionRuntime",
            resolvedRuntimeRoot,
            HostRunMode.Production,
            "Production",
            "准备启动",
            [
                "概览",
                "运行",
                "日志"
            ]);
    }

    /// <summary>
    /// 创建面向 UI 的最小只读启动信息。
    /// </summary>
    public static ApplicationPresentationOptions CreatePresentationOptions(ApplicationOptions? options = null)
    {
        var resolvedOptions = options ?? CreateDefaultOptions();

        return new ApplicationPresentationOptions(
            resolvedOptions.DisplayName,
            "Overview",
            resolvedOptions.StartupMessage,
            resolvedOptions.ReadOnlyEntryPoints ?? Array.Empty<string>());
    }

    /// <summary>
    /// 创建应用外部接线边界。
    /// </summary>
    public static ApplicationBoundary CreateBoundary(
        IConfigurationProvider? configurationProvider = null,
        IConfigurationSnapshot? configurationSnapshot = null,
        ILoggerWriter? loggerWriter = null,
        ILoggerContext? loggerContext = null)
    {
        return new ApplicationBoundary(configurationProvider, configurationSnapshot, loggerWriter, loggerContext);
    }

    /// <summary>
    /// 创建应用启动上下文。
    /// </summary>
    public static ApplicationBootstrapContext CreateBootstrapContext(
        ApplicationBoundary? boundary = null,
        ApplicationOptions? options = null,
        ApplicationPresentationOptions? presentationOptions = null,
        IReadOnlyCollection<IFusionModule>? modules = null)
    {
        var resolvedOptions = options ?? CreateDefaultOptions();
        var resolvedBoundary = boundary ?? CreateBoundary();

        return new ApplicationBootstrapContext(
            new HostBootstrapContext(
                resolvedBoundary.ConfigurationProvider,
                resolvedBoundary.ConfigurationSnapshot,
                resolvedBoundary.LoggerWriter,
                resolvedBoundary.LoggerContext),
            resolvedOptions,
            presentationOptions ?? CreatePresentationOptions(resolvedOptions),
            modules ?? Array.Empty<IFusionModule>());
    }

    /// <summary>
    /// 创建应用运行摘要。
    /// </summary>
    public static ApplicationRuntimeDescriptor CreateRuntimeDescriptor(
        ApplicationBootstrapContext? bootstrapContext = null)
    {
        var context = bootstrapContext ?? CreateBootstrapContext();
        var moduleNames = context.Modules
            .Select(module => module.Name)
            .ToArray();

        return new ApplicationRuntimeDescriptor(
            context.Options.ApplicationId,
            context.Options.ApplicationTitle,
            context.Options.HostId,
            context.Options.HostName,
            context.Options.RuntimeInstanceId,
            context.Options.RuntimeRoot,
            context.Options.RunMode,
            context.Options.Profile,
            context.PresentationOptions.StartupMessage,
            context.PresentationOptions.StartRoute,
            context.PresentationOptions.ReadOnlyEntryPoints.ToArray(),
            moduleNames);
    }

    /// <summary>
    /// 创建宿主构造器，便于上层自行决定何时生成运行体。
    /// </summary>
    public static HostRuntimeBuilder CreateHostBuilder(
        ApplicationBootstrapContext? bootstrapContext = null)
    {
        var context = bootstrapContext ?? CreateBootstrapContext();
        var hostOptions = new HostCompositionOptions(
            context.Options.HostId,
            context.Options.HostName,
            context.Options.RuntimeInstanceId,
            context.Options.RuntimeRoot.PhysicalRoot,
            context.Options.RunMode,
            context.Options.Profile);

        var hostBuilder = HostCompositionRoot.CreateBuilder(hostOptions, context.HostBootstrapContext);

        foreach (var module in context.Modules)
        {
            hostBuilder.AddModule(module);
        }

        return hostBuilder;
    }

    /// <summary>
    /// 构建应用运行体。
    /// </summary>
    public static ApplicationRuntime Build(
        ApplicationBootstrapContext? bootstrapContext = null)
    {
        var context = bootstrapContext ?? CreateBootstrapContext();
        var hostBuilder = CreateHostBuilder(context);
        var host = hostBuilder.Build();
        var descriptor = CreateRuntimeDescriptor(context);

        return new ApplicationRuntime(descriptor, host);
    }
}
