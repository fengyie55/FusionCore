using FusionDomain.Enums;
using FusionDomain.ValueObjects;
using FusionEquipment.Abstractions.Context;
using FusionEquipment.Abstractions.Contracts;
using FusionEquipment.Abstractions.Enums;

namespace FusionEquipment.Abstractions.Tests;

public sealed class ModuleAbstractionSkeletonTests
{
    [Fact]
    public void Equipment_Module_Contract_Compiles_And_Exposes_Minimal_Metadata()
    {
        var module = new StubProcessModule();

        Assert.Equal("EQ-ABS", module.EquipmentId.Value);
        Assert.Contains(ModuleCapability.Processing, module.Capabilities);
        Assert.Equal(ModuleType.Process, module.ModuleType);
    }

    [Fact]
    public void Module_Context_CanBeInstantiated()
    {
        var context = new ModuleContext(new EquipmentId("EQ-CTX"), new ModuleId("TM-01"), "Transfer Module 01");

        Assert.Equal("EQ-CTX", context.EquipmentId.Value);
        Assert.Equal("TM-01", context.ModuleId.Value);
    }

    private sealed class StubProcessModule : IProcessModule
    {
        public EquipmentId EquipmentId { get; } = new("EQ-ABS");

        public ModuleId ModuleId { get; } = new("PM-ABS");

        public string Name { get; } = "Stub PM";

        public ModuleType ModuleType { get; } = ModuleType.Process;

        public ModuleContext Context { get; } = new(new EquipmentId("EQ-ABS"), new ModuleId("PM-ABS"), "Stub PM");

        public IReadOnlyCollection<ModuleCapability> Capabilities { get; } = new[] { ModuleCapability.Processing };

        public bool IsEnabled { get; } = true;
    }
}
