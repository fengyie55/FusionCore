using FusionKernel.Context;
using FusionKernel.Hosting;
using FusionKernel.Lifecycle;
using FusionKernel.Modules;
using FusionKernel.Results;
using FusionKernel.Services;

namespace FusionKernel.Tests;

public sealed class KernelDefaultImplementationsTests
{
    [Fact]
    public void Fusion_Host_Builder_Can_Build_Minimal_Host()
    {
        var runtimeContext = new RuntimeContext("RT-02", @"R:\Fusion", HostRunMode.Development, "DEV");
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
        Assert.Equal(LifecycleState.Created, host.State);
    }

    [Fact]
    public void In_Memory_Module_Registry_Can_Register_And_Query_Module()
    {
        var registry = new InMemoryFusionModuleRegistry();
        var module = new PlatformModule();

        var result = registry.Register(module);
        var byId = registry.TryGetModule("FusionKernel.PlatformModule", out var moduleById);
        var byName = registry.TryGetModuleByName("FusionKernel Platform Module", out var moduleByName);

        Assert.True(result.Succeeded);
        Assert.True(byId);
        Assert.True(byName);
        Assert.Same(module, moduleById);
        Assert.Same(module, moduleByName);
        Assert.Single(registry.GetRegisteredModules());
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
    public async Task Default_Host_And_Result_Models_Keep_Minimal_Relationship()
    {
        var runtimeContext = new RuntimeContext("RT-03", @"R:\Fusion", HostRunMode.Simulation, "TEST");
        var serviceRegistry = new InMemoryServiceRegistry();
        var moduleRegistry = new InMemoryFusionModuleRegistry();
        moduleRegistry.Register(new PlatformModule());

        var host = new FusionHost(
            new FusionHostContext("HOST-03", "Kernel Host", HostRunMode.Simulation, @"R:\Fusion", runtimeContext),
            runtimeContext,
            moduleRegistry,
            serviceRegistry,
            serviceRegistry);

        var initializationResult = host.InitializeHost();
        await host.StartAsync();
        var startResult = new HostStartResult(true, host.Id, "HOST_STARTED", null);
        await host.StopAsync();

        Assert.True(initializationResult.Succeeded);
        Assert.Equal("HOST-03", initializationResult.HostId);
        Assert.True(startResult.Succeeded);
        Assert.Equal("HOST-03", startResult.HostId);
        Assert.Equal(LifecycleState.Stopped, host.State);
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
