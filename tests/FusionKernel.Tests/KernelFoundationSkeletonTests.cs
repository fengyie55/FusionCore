using FusionKernel.Hosting;
using FusionKernel.Modules;
using FusionKernel.Results;
using FusionKernel.Runtime;
using FusionKernel.Services;

namespace FusionKernel.Tests;

public sealed class KernelFoundationSkeletonTests
{
    [Fact]
    public void Runtime_And_Result_Models_Can_Be_Instantiated()
    {
        var runtimeContext = new RuntimeContext(
            new RuntimeInstanceId("RT-01"),
            @"R:\",
            HostRunMode.Development,
            "DEV");
        var operationResult = new OperationResult(true, "OK", null);
        var activationResult = new ModuleActivationResult(true, "MOD-01", "Activated", null);
        var initializationResult = new HostInitializationResult(true, "HOST-01", "Initialized", null, HostInitializationState.Initialized);
        var startResult = new HostStartResult(true, "HOST-01", "Started", null, HostState.Started);
        var stopResult = new HostStopResult(true, "HOST-01", "Stopped", null);
        var serviceRegistrationResult = new ServiceRegistrationResult(true, typeof(object), ServiceLifetimeKind.Singleton, null);
        var moduleRegistrationResult = new ModuleRegistrationResult(true, "MOD-01", null);
        var moduleInitializationResult = new ModuleInitializationResult(true, "MOD-01", "Initialized", null);
        var moduleStartResult = new ModuleStartResult(true, "MOD-01", "Started", null);
        var moduleStopResult = new ModuleStopResult(true, "MOD-01", "Stopped", null);

        Assert.Equal("RT-01", runtimeContext.RuntimeId);
        Assert.True(operationResult.Succeeded);
        Assert.Equal("MOD-01", activationResult.ModuleId);
        Assert.Equal("HOST-01", initializationResult.HostId);
        Assert.Equal("HOST-01", startResult.HostId);
        Assert.Equal("HOST-01", stopResult.HostId);
        Assert.Equal(ServiceLifetimeKind.Singleton, serviceRegistrationResult.Lifetime);
        Assert.Equal("MOD-01", moduleRegistrationResult.ModuleId);
        Assert.Equal("MOD-01", moduleInitializationResult.ModuleId);
        Assert.Equal("MOD-01", moduleStartResult.ModuleId);
        Assert.Equal("MOD-01", moduleStopResult.ModuleId);
    }

    [Fact]
    public void Core_Interfaces_Can_Be_Implemented_With_Minimal_Stubs()
    {
        IFusionHostContext hostContext = new StubHostContext();
        IFusionHost host = new StubHost(hostContext);
        IFusionHostBuilder builder = new StubHostBuilder(host);
        IFusionModuleRegistry registry = new StubModuleRegistry();
        IServiceRegistrar registrar = new StubServiceRegistrar();
        IServiceResolver resolver = new StubServiceResolver();
        var module = new PlatformModule();

        var builtHost = builder
            .UseRuntimeContext(new RuntimeContext(new RuntimeInstanceId("RT-01"), @"R:\", HostRunMode.Development, "DEV"))
            .UseHostContext(hostContext)
            .Build();
        var registrationResult = registry.Register(module);
        module.ConfigureServices(registrar);
        var resolved = resolver.Resolve(typeof(string));
        var initializeResult = builtHost.InitializeHost();
        var startResult = builtHost.StartHost();
        var stopResult = builtHost.StopHost();

        Assert.Equal("HOST-01", builtHost.Context.HostId);
        Assert.True(registrationResult.Succeeded);
        Assert.NotNull(resolved);
        Assert.True(initializeResult.Succeeded);
        Assert.True(startResult.Succeeded);
        Assert.True(stopResult.Succeeded);
        Assert.Single(registry.GetRegisteredModules());
    }

    private sealed class StubHostContext : IFusionHostContext
    {
        public string HostId { get; } = "HOST-01";

        public string HostName { get; } = "Fusion Host";

        public HostRunMode RunMode { get; } = HostRunMode.Development;

        public string RuntimeRoot { get; } = @"R:\";
    }

    private sealed class StubHost : IFusionHost
    {
        public StubHost(IFusionHostContext context)
        {
            Context = context;
            Descriptor = new HostDescriptor(context.HostId, context.HostName, "RT-01", context.RuntimeRoot, context.RunMode, "DEV");
            RuntimeContext = new RuntimeContext(new RuntimeInstanceId("RT-01"), context.RuntimeRoot, context.RunMode, "DEV");
        }

        public string Id { get; } = "HOST-01";

        public string Name { get; } = "Fusion Host";

        public IFusionHostContext Context { get; }

        public HostDescriptor Descriptor { get; }

        public RuntimeContext RuntimeContext { get; }

        public HostState State { get; } = HostState.Constructed;

        public HostInitializationState InitializationState { get; } = HostInitializationState.NotInitialized;

        public HostDiagnosticInfo DiagnosticInfo =>
            new(
                Descriptor,
                RuntimeContext.Descriptor,
                State,
                InitializationState,
                Array.Empty<FusionKernel.Composition.HostDependencyDescriptor>(),
                new ModuleCollectionSnapshot(Array.Empty<IFusionModuleDescriptor>(), new Dictionary<string, ModuleState>()));

        public void Initialize()
        {
        }

        public HostInitializationResult InitializeHost()
        {
            return new HostInitializationResult(true, Id, "Initialized", null, HostInitializationState.Initialized);
        }

        public HostStartResult StartHost()
        {
            return new HostStartResult(true, Id, "Started", null, HostState.Started);
        }

        public HostStopResult StopHost()
        {
            return new HostStopResult(true, Id, "Stopped", null, HostState.Stopped);
        }

        public ValueTask StartAsync(CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask StopAsync(CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }
    }

    private sealed class StubHostBuilder : IFusionHostBuilder
    {
        private readonly IFusionHost _host;

        public StubHostBuilder(IFusionHost host)
        {
            _host = host;
        }

        public IFusionHostBuilder UseRuntimeContext(RuntimeContext runtimeContext)
        {
            return this;
        }

        public IFusionHostBuilder UseHostContext(IFusionHostContext hostContext)
        {
            return this;
        }

        public IFusionHost Build()
        {
            return _host;
        }
    }

    private sealed class StubModuleRegistry : IFusionModuleRegistry
    {
        private readonly List<IFusionModuleDescriptor> _descriptors = new();
        private readonly Dictionary<string, IFusionModule> _modulesById = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> _moduleIdsByName = new(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, ModuleState> _statesById = new(StringComparer.OrdinalIgnoreCase);

        public ModuleRegistrationResult Register(IFusionModule module)
        {
            _descriptors.Add(module.Descriptor);
            _modulesById[module.Descriptor.ModuleId] = module;
            _moduleIdsByName[module.Descriptor.ModuleName] = module.Descriptor.ModuleId;
            _statesById[module.Descriptor.ModuleId] = ModuleState.Registered;
            return new ModuleRegistrationResult(true, module.Descriptor.ModuleId, null);
        }

        public IReadOnlyCollection<IFusionModuleDescriptor> GetRegisteredModules()
        {
            return _descriptors;
        }

        public IReadOnlyCollection<IFusionModule> GetModules()
        {
            return _modulesById.Values.ToArray();
        }

        public bool TryGetModule(string moduleId, out IFusionModule? module)
        {
            var found = _modulesById.TryGetValue(moduleId, out var registeredModule);
            module = registeredModule;
            return found;
        }

        public bool TryGetModuleByName(string moduleName, out IFusionModule? module)
        {
            module = null;

            if (!_moduleIdsByName.TryGetValue(moduleName, out var moduleId))
            {
                return false;
            }

            return TryGetModule(moduleId, out module);
        }

        public ModuleState GetModuleState(string moduleId)
        {
            return _statesById.TryGetValue(moduleId, out var state) ? state : ModuleState.Registered;
        }

        public bool TryUpdateState(string moduleId, ModuleState state)
        {
            if (!_modulesById.ContainsKey(moduleId))
            {
                return false;
            }

            _statesById[moduleId] = state;
            return true;
        }

        public ModuleCollectionSnapshot CreateSnapshot()
        {
            return new ModuleCollectionSnapshot(_descriptors, _statesById);
        }
    }

    private sealed class StubServiceRegistrar : IServiceRegistrar
    {
        public ServiceRegistrationResult Register(Type serviceType, Type implementationType, ServiceLifetimeKind lifetime)
        {
            return new ServiceRegistrationResult(true, serviceType, lifetime, implementationType.Name);
        }
    }

    private sealed class StubServiceResolver : IServiceResolver
    {
        public object? Resolve(Type serviceType)
        {
            return serviceType == typeof(string) ? "resolved" : null;
        }
    }
}
