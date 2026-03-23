using FusionDomain.Aggregates;
using FusionDomain.Entities;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Tests;

public sealed class DomainObjectSkeletonTests
{
    [Fact]
    public void Equipment_Aggregate_CanBeInstantiated()
    {
        var module = new EquipmentModule(new ModuleId("PM1"), "ProcessModule1", ModuleType.Process);
        var equipment = new Equipment(
            new EquipmentId("EQ-01"),
            "FusionCore",
            EquipmentState.Idle,
            new[] { module });

        Assert.Equal("FusionCore", equipment.Name);
        Assert.Single(equipment.Modules);
    }

    [Fact]
    public void Job_Aggregates_CanBeInstantiated()
    {
        var controlJob = new ControlJob(new ControlJobId("CJ-01"), ControlState.Created);
        var processJob = new ProcessJob(new ProcessJobId("PJ-01"), ControlState.Queued, new RecipeId("RC-01"));

        Assert.Equal(ControlState.Created, controlJob.State);
        Assert.Equal("RC-01", processJob.RecipeId.Value);
    }

    [Fact]
    public void Material_Entities_CanBeInstantiated()
    {
        var carrier = new Carrier(new CarrierId("C-01"), MaterialState.Registered);
        var substrate = new Substrate(new SubstrateId("S-01"), MaterialState.Waiting);

        Assert.Equal("C-01", carrier.CarrierId.Value);
        Assert.Equal("S-01", substrate.SubstrateId.Value);
    }

    [Fact]
    public void Alarm_And_Recipe_CanBeInstantiated()
    {
        var alarm = new Alarm(new AlarmId("AL-01"), "VAC_LOW", AlarmSeverity.Warning);
        var recipe = new Recipe(new RecipeId("RECIPE-01"), "Baseline Recipe");

        Assert.Equal(AlarmSeverity.Warning, alarm.Severity);
        Assert.Equal("Baseline Recipe", recipe.Name);
    }
}
