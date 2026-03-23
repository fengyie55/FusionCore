using FusionDomain.Aggregates;
using FusionDomain.Entities;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;
using FusionFA.Commands;
using FusionFA.Common;
using FusionFA.Contracts;
using FusionFA.Events;
using FusionFA.Models;
using FusionFA.Queries;
using FusionFA.States;

namespace FusionFA.Tests;

public sealed class AutomationMappingSkeletonTests
{
    [Fact]
    public void 自动化映射模型可以实例化()
    {
        var snapshot = new EquipmentAutomationSnapshot(
            new EquipmentId("EQ-01"),
            "FusionCore",
            "RUN",
            "ACTIVE",
            AutomationConnectionState.Connected,
            AutomationAvailabilityState.Available);
        var jobView = new AutomationJobView(
            new ControlJobId("CJ-01"),
            new ProcessJobId("PJ-01"),
            new RecipeId("RC-01"),
            "QUEUED",
            "READY");
        var materialView = new AutomationMaterialView(
            new MaterialId("MAT-01"),
            "Substrate",
            "WAIT",
            "LP1");
        var alarmView = new AutomationAlarmView(
            new AlarmId("AL-01"),
            "VAC_LOW",
            "WARN",
            "真空偏低");
        var recipeView = new AutomationRecipeView(
            new RecipeId("RC-01"),
            "Baseline Recipe",
            "ACTIVE");
        var request = new RemoteCommandRequest(
            "START",
            AutomationObjectType.Job,
            "CJ-01",
            new EquipmentId("EQ-01"),
            new Dictionary<string, string>());
        var result = new RemoteCommandResult(
            "START",
            RemoteCommandExecutionState.Accepted,
            "已接收");

        Assert.Equal("EQ-01", snapshot.EquipmentId.Value);
        Assert.Equal("CJ-01", jobView.ControlJobId.Value);
        Assert.Equal("MAT-01", materialView.MaterialId.Value);
        Assert.Equal("VAC_LOW", alarmView.AlarmCode);
        Assert.Equal("Baseline Recipe", recipeView.RecipeName);
        Assert.Equal(AutomationObjectType.Job, request.TargetType);
        Assert.Equal(RemoteCommandExecutionState.Accepted, result.State);
    }

    [Fact]
    public void 命令查询和事件可以创建()
    {
        var equipment = new Equipment(
            new EquipmentId("EQ-01"),
            "FusionCore",
            EquipmentState.Running,
            Array.Empty<EquipmentModule>());
        var alarm = new Alarm(new AlarmId("AL-01"), "VAC_LOW", AlarmSeverity.Warning);
        var controlJob = new ControlJob(new ControlJobId("CJ-01"), ControlState.Active);
        var processJob = new ProcessJob(new ProcessJobId("PJ-01"), ControlState.Active, new RecipeId("RC-01"));
        var material = new Material(new MaterialId("MAT-01"), MaterialState.InProcess);

        var publishEquipment = new PublishEquipmentStateCommand(equipment, ControlState.Active);
        var publishAlarm = new PublishAlarmCommand(alarm);
        var publishJob = new PublishJobStateCommand(controlJob, processJob);
        var publishMaterial = new PublishMaterialStateCommand(material);
        var executeRemote = new ExecuteRemoteCommandRequest(
            new RemoteCommandRequest(
                "ABORT",
                AutomationObjectType.Job,
                "CJ-01",
                new EquipmentId("EQ-01"),
                new Dictionary<string, string>()));

        var snapshotQuery = new GetCurrentEquipmentAutomationSnapshotQuery(new EquipmentId("EQ-01"));
        var jobQuery = new GetPublishedJobViewQuery(new ControlJobId("CJ-01"), new ProcessJobId("PJ-01"));
        var materialQuery = new GetPublishedMaterialViewQuery(new MaterialId("MAT-01"));
        var alarmQuery = new GetPublishedAlarmViewQuery(new AlarmId("AL-01"));

        var equipmentEvent = new EquipmentStatePublishedEvent(new EquipmentId("EQ-01"), "RUN");
        var alarmEvent = new AlarmPublishedEvent(new AlarmId("AL-01"), "VAC_LOW");
        var jobEvent = new JobPublishedEvent(new ControlJobId("CJ-01"), new ProcessJobId("PJ-01"));
        var materialEvent = new MaterialPublishedEvent(new MaterialId("MAT-01"), "Material");
        var recipeEvent = new RecipePublishedEvent(new RecipeId("RC-01"), "Baseline Recipe");

        Assert.Equal("EQ-01", publishEquipment.Equipment.Id.Value);
        Assert.Equal(ControlState.Active, publishEquipment.ControlState);
        Assert.Equal("VAC_LOW", publishAlarm.Alarm.Code);
        Assert.Equal("PJ-01", publishJob.ProcessJob.Id.Value);
        Assert.Equal("MAT-01", publishMaterial.Material.Id.Value);
        Assert.Equal("ABORT", executeRemote.Request.CommandName);
        Assert.Equal("EQ-01", snapshotQuery.EquipmentId.Value);
        Assert.Equal("CJ-01", jobQuery.ControlJobId.Value);
        Assert.Equal("MAT-01", materialQuery.MaterialId.Value);
        Assert.Equal("AL-01", alarmQuery.AlarmId.Value);
        Assert.Equal("RUN", equipmentEvent.PublishedStateCode);
        Assert.Equal("VAC_LOW", alarmEvent.AlarmCode);
        Assert.Equal("PJ-01", jobEvent.ProcessJobId.Value);
        Assert.Equal("Material", materialEvent.MaterialType);
        Assert.Equal("Baseline Recipe", recipeEvent.RecipeName);
    }

    [Fact]
    public void 服务契约可以由最小桩实现()
    {
        var service = new StubFAService();
        var stateProvider = new StubEquipmentStateProvider();
        var alarmPublisher = new StubAlarmPublisher();
        var jobMapper = new StubJobMapper();
        var materialMapper = new StubMaterialMapper();
        var recipeMapper = new StubRecipeMapper();
        var gateway = new StubRemoteCommandGateway();

        var equipment = new Equipment(
            new EquipmentId("EQ-01"),
            "FusionCore",
            EquipmentState.Running,
            Array.Empty<EquipmentModule>());
        var alarm = new Alarm(new AlarmId("AL-01"), "VAC_LOW", AlarmSeverity.Warning);
        var controlJob = new ControlJob(new ControlJobId("CJ-01"), ControlState.Active);
        var processJob = new ProcessJob(new ProcessJobId("PJ-01"), ControlState.Active, new RecipeId("RC-01"));
        var material = new Material(new MaterialId("MAT-01"), MaterialState.InProcess);
        var recipe = new Recipe(new RecipeId("RC-01"), "Baseline Recipe");

        var snapshot = stateProvider.CreateSnapshot(equipment, ControlState.Active);
        var publishedAlarm = alarmPublisher.Publish(alarm);
        var jobView = jobMapper.Map(controlJob, processJob);
        var materialView = materialMapper.Map(material);
        var recipeView = recipeMapper.Map(recipe);
        var commandResult = gateway.Execute(
            new RemoteCommandRequest(
                "START",
                AutomationObjectType.Job,
                "CJ-01",
                new EquipmentId("EQ-01"),
                new Dictionary<string, string>()));
        var serviceSnapshot = service.GetCurrentSnapshot();

        Assert.Equal("EQ-01", snapshot.EquipmentId.Value);
        Assert.Equal("VAC_LOW", publishedAlarm.AlarmCode);
        Assert.Equal("PJ-01", jobView.ProcessJobId.Value);
        Assert.Equal("MAT-01", materialView.MaterialId.Value);
        Assert.Equal("RC-01", recipeView.RecipeId.Value);
        Assert.Equal(RemoteCommandExecutionState.Accepted, commandResult.State);
        Assert.Equal("EQ-01", serviceSnapshot.EquipmentId.Value);
        Assert.Equal("ACTIVE", serviceSnapshot.ControlStateCode);
    }

    private sealed class StubFAService : IFAService
    {
        public EquipmentAutomationSnapshot GetCurrentSnapshot()
        {
            return new EquipmentAutomationSnapshot(
                new EquipmentId("EQ-01"),
                "FusionCore",
                "RUN",
                "ACTIVE",
                AutomationConnectionState.Connected,
                AutomationAvailabilityState.Available);
        }

        public EquipmentAutomationSnapshot PublishEquipmentState(PublishEquipmentStateCommand command)
        {
            return new EquipmentAutomationSnapshot(
                command.Equipment.Id,
                command.Equipment.Name,
                command.Equipment.State.ToString(),
                command.ControlState.ToString(),
                AutomationConnectionState.Connected,
                AutomationAvailabilityState.Available);
        }

        public AutomationAlarmView PublishAlarm(PublishAlarmCommand command)
        {
            return new AutomationAlarmView(
                command.Alarm.Id,
                command.Alarm.Code,
                command.Alarm.Severity.ToString(),
                command.Alarm.Code);
        }

        public AutomationJobView PublishJobState(PublishJobStateCommand command)
        {
            return new AutomationJobView(
                command.ControlJob.Id,
                command.ProcessJob.Id,
                command.ProcessJob.RecipeId,
                command.ControlJob.State.ToString(),
                command.ProcessJob.State.ToString());
        }

        public AutomationMaterialView PublishMaterialState(PublishMaterialStateCommand command)
        {
            return new AutomationMaterialView(
                command.Material.Id,
                command.Material.GetType().Name,
                command.Material.State.ToString(),
                null);
        }

        public RemoteCommandResult ExecuteRemoteCommand(ExecuteRemoteCommandRequest command)
        {
            return new RemoteCommandResult(
                command.Request.CommandName,
                RemoteCommandExecutionState.Accepted,
                "已接收");
        }
    }

    private sealed class StubEquipmentStateProvider : IEquipmentStateProvider
    {
        public EquipmentAutomationSnapshot CreateSnapshot(Equipment equipment, ControlState controlState)
        {
            return new EquipmentAutomationSnapshot(
                equipment.Id,
                equipment.Name,
                equipment.State.ToString(),
                controlState.ToString(),
                AutomationConnectionState.Connected,
                AutomationAvailabilityState.Available);
        }
    }

    private sealed class StubAlarmPublisher : IAlarmPublisher
    {
        public AutomationAlarmView Publish(Alarm alarm)
        {
            return new AutomationAlarmView(alarm.Id, alarm.Code, alarm.Severity.ToString(), alarm.Code);
        }
    }

    private sealed class StubJobMapper : IJobMapper
    {
        public AutomationJobView Map(ControlJob controlJob, ProcessJob processJob)
        {
            return new AutomationJobView(
                controlJob.Id,
                processJob.Id,
                processJob.RecipeId,
                controlJob.State.ToString(),
                processJob.State.ToString());
        }
    }

    private sealed class StubMaterialMapper : IMaterialMapper
    {
        public AutomationMaterialView Map(Material material)
        {
            return new AutomationMaterialView(material.Id, material.GetType().Name, material.State.ToString(), null);
        }
    }

    private sealed class StubRecipeMapper : IRecipeMapper
    {
        public AutomationRecipeView Map(Recipe recipe)
        {
            return new AutomationRecipeView(recipe.Id, recipe.Name, "ACTIVE");
        }
    }

    private sealed class StubRemoteCommandGateway : IRemoteCommandGateway
    {
        public RemoteCommandResult Execute(RemoteCommandRequest request)
        {
            return new RemoteCommandResult(request.CommandName, RemoteCommandExecutionState.Accepted, "已接收");
        }
    }
}
