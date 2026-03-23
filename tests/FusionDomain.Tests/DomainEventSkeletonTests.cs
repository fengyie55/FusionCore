using FusionDomain.Enums;
using FusionDomain.Events;
using FusionDomain.Events.Alarm;
using FusionDomain.Events.Carrier;
using FusionDomain.Events.Common;
using FusionDomain.Events.Equipment;
using FusionDomain.Events.Job;
using FusionDomain.Events.Material;
using FusionDomain.Events.Recipe;
using FusionDomain.ValueObjects;

namespace FusionDomain.Tests;

public sealed class DomainEventSkeletonTests
{
    [Fact]
    public void 设备事件可以实例化()
    {
        var equipmentEvent = new EquipmentStateChangedEvent(
            new EquipmentId("EQ-01"),
            EquipmentState.Idle,
            EquipmentState.Running);
        var controlEvent = new ControlStateChangedEvent(
            new EquipmentId("EQ-01"),
            ControlState.Created,
            ControlState.Active);

        Assert.Equal("EQ-01", equipmentEvent.EquipmentId.Value);
        Assert.Equal(EquipmentState.Running, equipmentEvent.CurrentState);
        Assert.Equal(ControlState.Active, controlEvent.CurrentState);
        Assert.IsAssignableFrom<DomainEvent>(equipmentEvent);
        Assert.IsAssignableFrom<DomainEvent>(controlEvent);
    }

    [Fact]
    public void 物料和载具事件可以实例化()
    {
        var previousLocation = new LocationReference(new ModuleId("BM-01"), "SLOT-01");
        var currentLocation = new LocationReference(new ModuleId("PM-01"), "CHAMBER");
        var createdEvent = new MaterialCreatedEvent(new MaterialId("MAT-01"), MaterialState.Registered);
        var stateChangedEvent = new MaterialStateChangedEvent(
            new MaterialId("MAT-01"),
            MaterialState.Waiting,
            MaterialState.InProcess);
        var locationChangedEvent = new MaterialLocationChangedEvent(
            new MaterialId("MAT-01"),
            previousLocation,
            currentLocation);
        var trackedEvent = new SubstrateTrackedEvent(new SubstrateId("SUB-01"), new MaterialId("MAT-01"));
        var loadedEvent = new CarrierLoadedEvent(
            new CarrierId("CAR-01"),
            new EquipmentId("EQ-01"),
            previousLocation);
        var unloadedEvent = new CarrierUnloadedEvent(
            new CarrierId("CAR-01"),
            new EquipmentId("EQ-01"),
            currentLocation);

        Assert.Equal(MaterialState.Registered, createdEvent.InitialState);
        Assert.Equal(MaterialState.InProcess, stateChangedEvent.CurrentState);
        Assert.Equal("PM-01", locationChangedEvent.CurrentLocation!.ModuleId!.Value);
        Assert.Equal("SUB-01", trackedEvent.SubstrateId.Value);
        Assert.Equal("CAR-01", loadedEvent.CarrierId.Value);
        Assert.Equal("EQ-01", unloadedEvent.EquipmentId.Value);
    }

    [Fact]
    public void 作业配方和告警事件可以实例化()
    {
        var controlCreated = new ControlJobCreatedEvent(new ControlJobId("CJ-01"));
        var controlStateChanged = new ControlJobStateChangedEvent(
            new ControlJobId("CJ-01"),
            ControlState.Created,
            ControlState.Active);
        var processCreated = new ProcessJobCreatedEvent(
            new ProcessJobId("PJ-01"),
            new RecipeId("RC-01"));
        var processStateChanged = new ProcessJobStateChangedEvent(
            new ProcessJobId("PJ-01"),
            ControlState.Queued,
            ControlState.Active);
        var recipeAssigned = new RecipeAssignedEvent(
            new RecipeId("RC-01"),
            new ProcessJobId("PJ-01"));
        var recipeChanged = new RecipeChangedEvent(
            new RecipeId("RC-OLD"),
            new RecipeId("RC-NEW"),
            new ProcessJobId("PJ-01"));
        var alarmRaised = new AlarmRaisedEvent(
            new AlarmId("AL-01"),
            "VAC_LOW",
            AlarmSeverity.Warning);
        var alarmCleared = new AlarmClearedEvent(
            new AlarmId("AL-01"),
            "VAC_LOW");

        Assert.Equal("CJ-01", controlCreated.ControlJobId.Value);
        Assert.Equal(ControlState.Active, controlStateChanged.CurrentState);
        Assert.Equal("RC-01", processCreated.RecipeId.Value);
        Assert.Equal(ControlState.Active, processStateChanged.CurrentState);
        Assert.Equal("PJ-01", recipeAssigned.ProcessJobId.Value);
        Assert.Equal("RC-NEW", recipeChanged.CurrentRecipeId.Value);
        Assert.Equal(AlarmSeverity.Warning, alarmRaised.Severity);
        Assert.Equal("VAC_LOW", alarmCleared.AlarmCode);
    }
}
