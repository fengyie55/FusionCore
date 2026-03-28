using FusionApp.Runtime;
using FusionConfig.Abstractions;
using FusionConfig.Runtime;
using FusionKernel.Composition;
using FusionKernel.Hosting;
using FusionKernel.Modules;
using FusionLog.Abstractions;

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
        var resolvedBoundary = boundary ?? CreateBoundary();
        var resolvedRuntimeRoot = ResolveRuntimeRoot(resolvedBoundary, options);
        var resolvedProfile = ResolveProfile(resolvedBoundary, options);
        var resolvedOptions = options ?? CreateDefaultOptions(resolvedRuntimeRoot);

        if (!string.IsNullOrWhiteSpace(resolvedProfile))
        {
            resolvedOptions = resolvedOptions with
            {
                Profile = resolvedProfile
            };
        }

        if (resolvedOptions.RuntimeRoot != resolvedRuntimeRoot)
        {
            resolvedOptions = resolvedOptions with
            {
                RuntimeRoot = resolvedRuntimeRoot
            };
        }

        return new ApplicationBootstrapContext(
            resolvedBoundary,
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
    /// 创建面向 UI 的最小启动描述。
    /// </summary>
    public static ApplicationUiBootstrapDescriptor CreateUiBootstrapDescriptor(
        ApplicationBootstrapContext? bootstrapContext = null)
    {
        var context = bootstrapContext ?? CreateBootstrapContext();
        var runtimeDescriptor = CreateRuntimeDescriptor(context);

        return new ApplicationUiBootstrapDescriptor(
            context.PresentationOptions.ShellTitle,
            context.PresentationOptions.StartRoute,
            context.PresentationOptions.StartupMessage,
            context.PresentationOptions.ReadOnlyEntryPoints.ToArray(),
            runtimeDescriptor);
    }

    /// <summary>
    /// 创建完整的应用装配结果。
    /// </summary>
    public static ApplicationAssembly CreateAssembly(
        ApplicationBootstrapContext? bootstrapContext = null)
    {
        var context = bootstrapContext ?? CreateBootstrapContext();
        var runtimeDescriptor = CreateRuntimeDescriptor(context);
        var uiBootstrapDescriptor = CreateUiBootstrapDescriptor(context);
        var runtime = new ApplicationRuntime(runtimeDescriptor, CreateHostBuilder(context).Build());

        return new ApplicationAssembly(
            context.Options,
            context.Boundary,
            context,
            runtimeDescriptor,
            uiBootstrapDescriptor,
            runtime);
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
    /// 创建宿主构造器。
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
        return CreateAssembly(bootstrapContext).Runtime;
    }

    /// <summary>
    /// 解析最终使用的运行根。
    /// </summary>
    private static RuntimeRootOptions ResolveRuntimeRoot(
        ApplicationBoundary boundary,
        ApplicationOptions? options)
    {
        if (options is not null)
        {
            return options.RuntimeRoot;
        }

        if (boundary.ConfigurationProvider is not null)
        {
            return boundary.ConfigurationProvider.GetRuntimeRoot();
        }

        if (boundary.ConfigurationSnapshot is not null)
        {
            return boundary.ConfigurationSnapshot.RuntimeRoot;
        }

        return RuntimeRootOptions.CreateDefault();
    }

    /// <summary>
    /// 解析最终使用的 profile。
    /// </summary>
    private static string? ResolveProfile(
        ApplicationBoundary boundary,
        ApplicationOptions? options)
    {
        if (options is not null && !string.IsNullOrWhiteSpace(options.Profile))
        {
            return options.Profile;
        }

        if (boundary.ConfigurationProvider is not null)
        {
            return boundary.ConfigurationProvider.GetProfile().ProfileName;
        }

        if (boundary.ConfigurationSnapshot is not null)
        {
            return boundary.ConfigurationSnapshot.Profile.ProfileName;
        }

        return null;
    }
}
