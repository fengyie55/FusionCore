using FusionApp.Composition;
using FusionApp.Runtime;
using FusionConfig.Abstractions;
using FusionConfig.Profiles;
using FusionConfig.Providers;
using FusionConfig.Runtime;
using FusionConfig.Snapshots;
using FusionKernel;
using FusionKernel.Hosting;
using FusionLog.Context;
using FusionLog.Writers;

namespace FusionApp.Tests;

public sealed class ApplicationCompositionRootTests
{
    [Fact]
    public void CreateDefaultOptions_UsesRuntimeRootAndEntryPoints()
    {
        var options = ApplicationCompositionRoot.CreateDefaultOptions();

        Assert.Equal("FusionApp", options.ApplicationId);
        Assert.Equal("FusionCore Application", options.ApplicationTitle);
        Assert.Equal(@"R:\", options.RuntimeRoot.LogicalRoot);
        Assert.Equal(HostRunMode.Production, options.RunMode);
        Assert.Equal(3, options.ReadOnlyEntryPoints?.Count);
    }

    [Fact]
    public void CreateBootstrapContext_CarriesConfigAndLogBoundaries()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(@"D:\FusionCore");
        var snapshot = new ConfigurationSnapshot(
            new EnvironmentProfile("Production", ConfigurationProfileKind.Production, "Production"),
            runtimeRoot,
            Array.Empty<IConfigurationSection>());

        var boundary = ApplicationCompositionRoot.CreateBoundary(
            new DefaultConfigurationProvider(snapshot),
            snapshot,
            new NullLoggerWriter(),
            new LogContext(
                new HostLogContext("FusionHost", "Fusion Host"),
                new ProcessLogContext("Process-01", "Fusion Process"),
                new ModuleLogContext("FusionApp", nameof(ApplicationCompositionRootTests), "Instance-01")));

        var context = ApplicationCompositionRoot.CreateBootstrapContext(boundary);

        Assert.NotNull(context.HostBootstrapContext.ConfigurationProvider);
        Assert.NotNull(context.HostBootstrapContext.ConfigurationSnapshot);
        Assert.NotNull(context.HostBootstrapContext.LoggerWriter);
        Assert.NotNull(context.HostBootstrapContext.LoggerContext);
        Assert.Equal(boundary.ConfigurationProvider, context.HostBootstrapContext.ConfigurationProvider);
        Assert.Equal(boundary, context.Boundary);
    }

    [Fact]
    public void CreateBootstrapContext_Uses_Configuration_RuntimeRoot_And_Profile_When_Boundary_Is_Provided()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(@"D:\FusionCore");
        var snapshot = new ConfigurationSnapshot(
            new EnvironmentProfile("sim", ConfigurationProfileKind.Simulation, "Simulation"),
            runtimeRoot,
            Array.Empty<IConfigurationSection>());
        var boundary = ApplicationCompositionRoot.CreateBoundary(
            new DefaultConfigurationProvider(snapshot),
            snapshot,
            new NullLoggerWriter(),
            new LogContext(
                new HostLogContext("FusionHost", "Fusion Host"),
                new ProcessLogContext("Process-01", "Fusion Process"),
                new ModuleLogContext("FusionApp", nameof(ApplicationCompositionRootTests), "Instance-01")));

        var context = ApplicationCompositionRoot.CreateBootstrapContext(boundary);

        Assert.Equal("sim", boundary.ConfigurationProvider!.GetProfile().ProfileName);
        Assert.Equal("sim", boundary.ConfigurationSnapshot!.Profile.ProfileName);
        Assert.Equal(runtimeRoot.PhysicalRoot, context.Options.RuntimeRoot.PhysicalRoot);
        Assert.Equal("sim", context.Options.Profile);
    }

    [Fact]
    public void Build_CreatesRuntimeAndHost()
    {
        var context = ApplicationCompositionRoot.CreateBootstrapContext(
            modules:
            [
                new PlatformModule()
            ]);

        var runtime = ApplicationCompositionRoot.Build(context);

        Assert.IsType<ApplicationRuntime>(runtime);
        Assert.Equal("FusionApp", runtime.Descriptor.ApplicationId);
        Assert.Equal("FusionHost", runtime.Descriptor.HostId);
        Assert.Equal("Overview", runtime.Descriptor.StartRoute);
        Assert.Single(runtime.Descriptor.ModuleNames);
        Assert.Equal(nameof(PlatformModule), runtime.Descriptor.ModuleNames[0]);
        Assert.Equal(HostState.Constructed, runtime.Host.State);
        Assert.Equal(HostInitializationState.NotInitialized, runtime.Host.InitializationState);
    }

    [Fact]
    public void CreateUiBootstrapDescriptor_Carries_Runtime_And_Presentation_Summaries()
    {
        var context = ApplicationCompositionRoot.CreateBootstrapContext(
            modules:
            [
                new PlatformModule()
            ]);

        var uiDescriptor = ApplicationCompositionRoot.CreateUiBootstrapDescriptor(context);

        Assert.Equal("FusionCore Application", uiDescriptor.DisplayTitle);
        Assert.Equal("Overview", uiDescriptor.StartRoute);
        Assert.Equal("准备启动", uiDescriptor.StartupMessage);
        Assert.Equal(3, uiDescriptor.ReadOnlyEntryPoints.Count);
        Assert.Equal(context.Options.ApplicationId, uiDescriptor.RuntimeDescriptor.ApplicationId);
        Assert.Single(uiDescriptor.RuntimeDescriptor.ModuleNames);
    }

    [Fact]
    public void CreateStudioBootstrapDescriptor_Carries_Runtime_And_Workbench_Summaries()
    {
        var context = ApplicationCompositionRoot.CreateBootstrapContext(
            modules:
            [
                new PlatformModule()
            ]);

        var studioDescriptor = ApplicationCompositionRoot.CreateStudioBootstrapDescriptor(context);

        Assert.Equal("FusionStudio", studioDescriptor.DisplayTitle);
        Assert.Equal("ConfigurationWorkbench", studioDescriptor.StartRoute);
        Assert.Equal("准备进入平台工程工作台", studioDescriptor.StartupMessage);
        Assert.Equal(context.Options.ApplicationId, studioDescriptor.RuntimeDescriptor.ApplicationId);
        Assert.Contains("LogsWorkbench", studioDescriptor.ReadOnlyEntryPoints);
    }

    [Fact]
    public void CreateAssembly_Binds_Config_Log_Host_And_Ui_Bootstrap_Descriptor()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(@"D:\FusionCore");
        var snapshot = new ConfigurationSnapshot(
            new EnvironmentProfile("dev", ConfigurationProfileKind.Development, "Development"),
            runtimeRoot,
            Array.Empty<IConfigurationSection>());
        var boundary = ApplicationCompositionRoot.CreateBoundary(
            new DefaultConfigurationProvider(snapshot),
            snapshot,
            new NullLoggerWriter(),
            new LogContext(
                new HostLogContext("FusionHost", "Fusion Host"),
                new ProcessLogContext("Process-01", "Fusion Process"),
                new ModuleLogContext("FusionApp", nameof(ApplicationCompositionRootTests), "Instance-01")));

        var assembly = ApplicationCompositionRoot.CreateAssembly(
            ApplicationCompositionRoot.CreateBootstrapContext(
                boundary,
                modules:
                [
                    new PlatformModule()
                ]));

        Assert.Equal(runtimeRoot.PhysicalRoot, assembly.Options.RuntimeRoot.PhysicalRoot);
        Assert.Equal("dev", assembly.Options.Profile);
        Assert.Equal(boundary.ConfigurationProvider, assembly.Boundary.ConfigurationProvider);
        Assert.Equal("FusionCore Application", assembly.UiBootstrapDescriptor.DisplayTitle);
        Assert.Equal("Overview", assembly.UiBootstrapDescriptor.StartRoute);
        Assert.Equal("FusionStudio", assembly.StudioBootstrapDescriptor.DisplayTitle);
        Assert.Equal("ConfigurationWorkbench", assembly.StudioBootstrapDescriptor.StartRoute);
        Assert.Single(assembly.UiBootstrapDescriptor.RuntimeDescriptor.ModuleNames);
        Assert.Single(assembly.StudioBootstrapDescriptor.RuntimeDescriptor.ModuleNames);
        Assert.Equal("FusionApp", assembly.RuntimeDescriptor.ApplicationId);
        Assert.Equal(HostState.Constructed, assembly.Runtime.Host.State);
    }

    [Fact]
    public void RuntimeLifecycle_DelegatesToHost()
    {
        var runtime = ApplicationCompositionRoot.Build(
            ApplicationCompositionRoot.CreateBootstrapContext(
                modules:
                [
                    new PlatformModule()
                ]));

        var initializationResult = runtime.Initialize();
        var startResult = runtime.Start();
        var stopResult = runtime.Stop();

        Assert.True(initializationResult.Succeeded);
        Assert.True(startResult.Succeeded);
        Assert.True(stopResult.Succeeded);
        Assert.Equal("MODULE_INITIALIZED", initializationResult.ModuleResults![0].Code);
        Assert.Equal("MODULE_STARTED", startResult.ModuleResults![0].Code);
        Assert.Equal("MODULE_STOPPED", stopResult.ModuleResults![0].Code);
    }
}
