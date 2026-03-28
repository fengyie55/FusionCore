using FusionApp.Composition;
using FusionApp.Runtime;
using FusionConfig.Abstractions;
using FusionConfig.Profiles;
using FusionConfig.Runtime;
using FusionConfig.Providers;
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
