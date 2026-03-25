using FusionKernel.Composition;
using FusionKernel.Hosting;
using FusionKernel.Runtime;

namespace FusionKernel.Tests;

public sealed class KernelHostLifecycleTests
{
    [Fact]
    public void Runtime_Context_Can_Express_Profile_Root_And_Instance_Id()
    {
        var context = new RuntimeContext(
            new RuntimeInstanceId("RT-LIFECYCLE-01"),
            @"R:\Fusion",
            HostRunMode.Development,
            "DEV",
            RuntimeStatus.Created,
            ConfigurationProvider: new object(),
            LoggerWriter: new object());

        Assert.Equal("RT-LIFECYCLE-01", context.RuntimeId);
        Assert.Equal(@"R:\Fusion", context.RuntimeRoot);
        Assert.Equal("DEV", context.Profile);
        Assert.NotNull(context.ConfigurationProvider);
        Assert.NotNull(context.LoggerWriter);
    }

    [Fact]
    public void Host_State_Changes_Correctly_During_Minimal_Lifecycle()
    {
        var host = HostCompositionRoot.CreateBuilder(
                new HostCompositionOptions("HOST-L1", "Lifecycle Host", "RT-L1", @"R:\Fusion", HostRunMode.Simulation, "SIM"))
            .AddModule(new PlatformModule())
            .Build();

        var initialized = host.InitializeHost();
        var started = host.StartHost();
        var stopped = host.StopHost();

        Assert.True(initialized.Succeeded);
        Assert.Equal(HostInitializationState.Initialized, host.InitializationState);
        Assert.True(started.Succeeded);
        Assert.Equal(HostState.Started, started.State);
        Assert.True(stopped.Succeeded);
        Assert.Equal(HostState.Stopped, stopped.State);
        Assert.Equal(RuntimeStatus.Stopped, host.RuntimeContext.Status);
    }

    [Fact]
    public void Diagnostic_Info_Can_Reflect_Host_And_Module_Snapshot()
    {
        var host = HostCompositionRoot.CreateBuilder(
                new HostCompositionOptions("HOST-DIAG", "Diagnostic Host", "RT-DIAG", @"R:\Fusion", HostRunMode.Production, "PROD"),
                new HostBootstrapContext(ConfigurationSnapshot: new object(), LoggerContext: new object()))
            .AddModule(new PlatformModule())
            .Build();

        host.InitializeHost();

        var diagnostic = host.DiagnosticInfo;

        Assert.Equal("HOST-DIAG", diagnostic.Host.HostId);
        Assert.Equal("RT-DIAG", diagnostic.Runtime.InstanceId.Value);
        Assert.Single(diagnostic.Modules.Modules);
        Assert.Contains(diagnostic.Dependencies, item => item.Name == "ConfigurationSnapshot" && item.IsConfigured);
        Assert.Contains(diagnostic.Dependencies, item => item.Name == "LoggerContext" && item.IsConfigured);
    }
}
