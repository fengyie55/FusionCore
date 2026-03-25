using FusionKernel.Composition;
using FusionKernel.Hosting;
using FusionKernel.Modules;
using FusionKernel.Results;
using FusionKernel.Runtime;
using FusionKernel.Services;

namespace FusionKernel.Tests;

public sealed class KernelDefaultImplementationsTests
{
    [Fact]
    public void Fusion_Host_Builder_Can_Build_Minimal_Host()
    {
        var runtimeContext = new RuntimeContext(
            new RuntimeInstanceId("RT-02"),
            @"R:\Fusion",
            HostRunMode.Development,
            "DEV");
        var hostContext = new FusionHostContext("HOST-02", "Kernel Host", HostRunMode.Development, @"R:\Fusion", runtimeContext);
        var moduleRegistry = new InMemoryFusionModuleRegistry();
        var serviceRegistry = new InMemoryServiceRegistry();
        var builder = new FusionHostBuilder();

        moduleRegistry.Register(new PlatformModule());

        builder
            .UseModuleRegistry(moduleRegistry)
            .UseServiceRegistrar(serviceRegistry)
            .UseServiceResolver(serviceRegistry)
            .UseRuntimeContext(runtimeContext)
            .UseHostContext(hostContext);

        var host = Assert.IsType<FusionHost>(builder.Build());

        Assert.Equal("HOST-02", host.Context.HostId);
        Assert.Same(moduleRegistry, host.ModuleRegistry);
        Assert.Same(serviceRegistry, host.ServiceRegistrar);
        Assert.Same(serviceRegistry, host.ServiceResolver);
        Assert.Equal(HostState.Constructed, host.State);
        Assert.Equal(HostInitializationState.NotInitialized, host.InitializationState);
    }

    [Fact]
    public void In_Memory_Module_Registry_Can_Register_Query_And_Update_State()
    {
        var registry = new InMemoryFusionModuleRegistry();
        var module = new PlatformModule();

        var result = registry.Register(module);
        var byId = registry.TryGetModule("FusionKernel.PlatformModule", out var moduleById);
        var byName = registry.TryGetModuleByName("FusionKernel Platform Module", out var moduleByName);
        var stateUpdated = registry.TryUpdateState(module.Descriptor.ModuleId, ModuleState.Started);

        Assert.True(result.Succeeded);
        Assert.True(byId);
        Assert.True(byName);
        Assert.True(stateUpdated);
        Assert.Same(module, moduleById);
        Assert.Same(module, moduleByName);
        Assert.Single(registry.GetRegisteredModules());
        Assert.Equal(ModuleState.Started, registry.GetModuleState(module.Descriptor.ModuleId));
    }

    [Fact]
    public void In_Memory_Service_Registry_Can_Register_And_Resolve_Service()
    {
        var registry = new InMemoryServiceRegistry();

        var registration = registry.Register(
            typeof(ISampleService),
            typeof(SampleService),
            ServiceLifetimeKind.Singleton);

        var first = Assert.IsType<SampleService>(registry.Resolve(typeof(ISampleService)));
        var second = Assert.IsType<SampleService>(registry.Resolve(typeof(ISampleService)));

        Assert.True(registration.Succeeded);
        Assert.Same(first, second);
        Assert.Equal("sample", first.Name);
    }

    [Fact]
    public void Host_Composition_Root_Can_Create_Default_Host()
    {
        var host = HostCompositionRoot.CreateBuilder(
                new HostCompositionOptions("HOST-05", "Fusion Host", "RT-05", @"R:\Fusion", HostRunMode.Simulation, "SIM"),
                new HostBootstrapContext(ConfigurationProvider: new object(), LoggerWriter: new object()))
            .AddModule(new PlatformModule())
            .Build();

        Assert.Equal("HOST-05", host.Context.HostId);
        Assert.Equal("RT-05", host.RuntimeContext.RuntimeId);
        Assert.Contains(host.DiagnosticInfo.Dependencies, item => item.Name == "ConfigurationProvider" && item.IsConfigured);
        Assert.Contains(host.DiagnosticInfo.Dependencies, item => item.Name == "LoggerWriter" && item.IsConfigured);
    }

    [Fact]
    public async Task Default_Host_Initialization_Start_And_Stop_Form_Minimal_Closure()
    {
        var runtimeContext = new RuntimeContext(
            new RuntimeInstanceId("RT-03"),
            @"R:\Fusion",
            HostRunMode.Simulation,
            "TEST");
        var serviceRegistry = new InMemoryServiceRegistry();
        var moduleRegistry = new InMemoryFusionModuleRegistry();
        moduleRegistry.Register(new PlatformModule());

        var host = new FusionHost(
            new FusionHostContext("HOST-03", "Kernel Host", HostRunMode.Simulation, @"R:\Fusion", runtimeContext),
            new HostDescriptor("HOST-03", "Kernel Host", "RT-03", @"R:\Fusion", HostRunMode.Simulation, "TEST"),
            runtimeContext,
            moduleRegistry,
            serviceRegistry,
            serviceRegistry);

        var initializationResult = host.InitializeHost();
        var startResult = host.StartHost();
        var stopResult = host.StopHost();
        await host.StartAsync();
        await host.StopAsync();

        Assert.True(initializationResult.Succeeded);
        Assert.Equal(HostInitializationState.Initialized, initializationResult.InitializationState);
        Assert.True(startResult.Succeeded);
        Assert.Equal(HostState.Started, startResult.State);
        Assert.True(stopResult.Succeeded);
        Assert.Equal(HostState.Stopped, stopResult.State);
        Assert.Equal(RuntimeStatus.Stopped, host.RuntimeContext.Status);
    }

    private interface ISampleService
    {
        string Name { get; }
    }

    private sealed class SampleService : ISampleService
    {
        public string Name { get; } = "sample";
    }
}
