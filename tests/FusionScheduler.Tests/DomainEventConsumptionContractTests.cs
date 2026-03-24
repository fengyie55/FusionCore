using FusionDomain.Enums;
using FusionDomain.Events.Alarm;
using FusionDomain.Events.Carrier;
using FusionDomain.Events.Common;
using FusionDomain.Events.Equipment;
using FusionDomain.Events.Job;
using FusionDomain.Events.Material;
using FusionDomain.Events.Recipe;
using FusionDomain.ValueObjects;
using FusionScheduler.EventConsumers;
using FusionScheduler.EventContracts;
using FusionScheduler.EventModels;
using FusionScheduler.EventUpdates;
using FusionScheduler.Recovery;

namespace FusionScheduler.Tests;

public sealed class DomainEventConsumptionContractTests
{
    [Fact]
    public void Update_Result_Models_Can_Be_Instantiated()
    {
        var routeRequest = new RouteRefreshRequest(
            new ProcessJobId("PJ-01"),
            new MaterialId("MAT-01"),
            new EquipmentId("EQ-01"),
            "MaterialLocationChanged");
        var trackingRequest = new TrackingRefreshRequest(
            new MaterialId("MAT-01"),
            new CarrierId("CAR-01"),
            new SubstrateId("SUB-01"),
            new ModuleId("BM-01"),
            "BM-01-SLOT-01");
        var recoveryRequest = new RecoveryEvaluationRequest(
            new ProcessJobId("PJ-01"),
            new MaterialId("MAT-01"),
            new AlarmId("AL-01"),
            new EquipmentId("EQ-01"),
            RecoveryReason.OperatorAbort);
        var jobRequest = new JobContextRefreshRequest(
            new ControlJobId("CJ-01"),
            new ProcessJobId("PJ-01"),
            new RecipeId("RC-01"),
            "ProcessJobCreated");
        var result = new SchedulerContextUpdateResult(
            new[] { routeRequest },
            new[] { trackingRequest },
            new[] { recoveryRequest },
            new[] { jobRequest });

        Assert.Single(result.RouteRefreshRequests);
        Assert.Single(result.TrackingRefreshRequests);
        Assert.Single(result.RecoveryEvaluationRequests);
        Assert.Single(result.JobContextRefreshRequests);
    }

    [Fact]
    public void Consumer_Contracts_Can_Be_Implemented_With_Domain_Events()
    {
        ISchedulerEventConsumptionContext context = new StubConsumptionContext();
        IEquipmentEventConsumer equipmentConsumer = new StubEquipmentEventConsumer();
        IMaterialEventConsumer materialConsumer = new StubMaterialEventConsumer();
        ICarrierEventConsumer carrierConsumer = new StubCarrierEventConsumer();
        IJobEventConsumer jobConsumer = new StubJobEventConsumer();
        IRecipeEventConsumer recipeConsumer = new StubRecipeEventConsumer();
        IAlarmEventConsumer alarmConsumer = new StubAlarmEventConsumer();

        var equipmentResult = equipmentConsumer.Consume(
            new EquipmentStateChangedEvent(
                new EquipmentId("EQ-01"),
                EquipmentState.Idle,
                EquipmentState.Running),
            context);
        var materialResult = materialConsumer.Consume(
            new MaterialLocationChangedEvent(
                new MaterialId("MAT-01"),
                new LocationReference(new ModuleId("BM-01"), "BM-01-SLOT-01"),
                new LocationReference(new ModuleId("PM-01"), "PM-01")),
            context);
        var carrierResult = carrierConsumer.Consume(
            new CarrierLoadedEvent(
                new CarrierId("CAR-01"),
                new EquipmentId("EQ-01"),
                new LocationReference(new ModuleId("CM-01"), "LP-01")),
            context);
        var jobResult = jobConsumer.Consume(
            new ProcessJobCreatedEvent(
                new ProcessJobId("PJ-01"),
                new RecipeId("RC-01")),
            context);
        var recipeResult = recipeConsumer.Consume(
            new RecipeAssignedEvent(
                new RecipeId("RC-01"),
                new ProcessJobId("PJ-01")),
            context);
        var alarmResult = alarmConsumer.Consume(
            new AlarmRaisedEvent(
                new AlarmId("AL-01"),
                "ALARM-01",
                AlarmSeverity.Error),
            context);

        Assert.Single(equipmentResult.RecoveryEvaluationRequests);
        Assert.Single(materialResult.TrackingRefreshRequests);
        Assert.Single(carrierResult.RouteRefreshRequests);
        Assert.Single(jobResult.JobContextRefreshRequests);
        Assert.Single(recipeResult.JobContextRefreshRequests);
        Assert.Single(alarmResult.RecoveryEvaluationRequests);
    }

    [Fact]
    public void Context_Updater_Contracts_Can_Be_Implemented()
    {
        IMaterialContextUpdater materialUpdater = new StubMaterialContextUpdater();
        IProductionJobContextUpdater jobUpdater = new StubProductionJobContextUpdater();
        IRouteContextUpdater routeUpdater = new StubRouteContextUpdater();
        IRecoveryContextUpdater recoveryUpdater = new StubRecoveryContextUpdater();

        var materialResult = materialUpdater.CreateUpdate(
            new MaterialStateChangedEvent(
                new MaterialId("MAT-01"),
                MaterialState.Waiting,
                MaterialState.InProcess));
        var jobResult = jobUpdater.CreateUpdate(
            new ControlJobStateChangedEvent(
                new ControlJobId("CJ-01"),
                ControlState.Created,
                ControlState.Active));
        var routeResult = routeUpdater.CreateUpdate(
            new CarrierUnloadedEvent(
                new CarrierId("CAR-01"),
                new EquipmentId("EQ-01"),
                new LocationReference(new ModuleId("CM-01"), "LP-01")));
        var recoveryResult = recoveryUpdater.CreateUpdate(
            new AlarmClearedEvent(
                new AlarmId("AL-01"),
                "ALARM-01"));

        Assert.Single(materialResult.TrackingRefreshRequests);
        Assert.Single(jobResult.JobContextRefreshRequests);
        Assert.Single(routeResult.RouteRefreshRequests);
        Assert.Single(recoveryResult.RecoveryEvaluationRequests);
    }

    private sealed class StubConsumptionContext : ISchedulerEventConsumptionContext
    {
        public DateTimeOffset ConsumedAtUtc { get; } = DateTimeOffset.UtcNow;

        public string ConsumerName { get; } = "StubConsumer";

        public string? SourceBoundary { get; } = "FusionDomain";
    }

    private sealed class StubEquipmentEventConsumer : IEquipmentEventConsumer
    {
        public SchedulerContextUpdateResult Consume(EquipmentStateChangedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateRecoveryResult(null, domainEvent.EquipmentId);
        }

        public SchedulerContextUpdateResult Consume(ControlStateChangedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateRecoveryResult(null, domainEvent.EquipmentId);
        }
    }

    private sealed class StubMaterialEventConsumer : IMaterialEventConsumer
    {
        public SchedulerContextUpdateResult Consume(MaterialCreatedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateTrackingResult(domainEvent.MaterialId, null, null);
        }

        public SchedulerContextUpdateResult Consume(MaterialStateChangedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateTrackingResult(domainEvent.MaterialId, null, null);
        }

        public SchedulerContextUpdateResult Consume(MaterialLocationChangedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateTrackingResult(domainEvent.MaterialId, domainEvent.CurrentLocation?.ModuleId, domainEvent.CurrentLocation?.LocationCode);
        }

        public SchedulerContextUpdateResult Consume(SubstrateTrackedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return new SchedulerContextUpdateResult(
                Array.Empty<RouteRefreshRequest>(),
                new[]
                {
                    new TrackingRefreshRequest(domainEvent.MaterialId, null, domainEvent.SubstrateId, null, null),
                },
                Array.Empty<RecoveryEvaluationRequest>(),
                Array.Empty<JobContextRefreshRequest>());
        }
    }

    private sealed class StubCarrierEventConsumer : ICarrierEventConsumer
    {
        public SchedulerContextUpdateResult Consume(CarrierLoadedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateRouteResult(null, null, domainEvent.EquipmentId, domainEvent.CarrierId.Value);
        }

        public SchedulerContextUpdateResult Consume(CarrierUnloadedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateRouteResult(null, null, domainEvent.EquipmentId, domainEvent.CarrierId.Value);
        }
    }

    private sealed class StubJobEventConsumer : IJobEventConsumer
    {
        public SchedulerContextUpdateResult Consume(ControlJobCreatedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateJobResult(domainEvent.ControlJobId, null, null, "ControlJobCreated");
        }

        public SchedulerContextUpdateResult Consume(ControlJobStateChangedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateJobResult(domainEvent.ControlJobId, null, null, "ControlJobStateChanged");
        }

        public SchedulerContextUpdateResult Consume(ProcessJobCreatedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, domainEvent.RecipeId, "ProcessJobCreated");
        }

        public SchedulerContextUpdateResult Consume(ProcessJobStateChangedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, null, "ProcessJobStateChanged");
        }
    }

    private sealed class StubRecipeEventConsumer : IRecipeEventConsumer
    {
        public SchedulerContextUpdateResult Consume(RecipeAssignedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, domainEvent.RecipeId, "RecipeAssigned");
        }

        public SchedulerContextUpdateResult Consume(RecipeChangedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, domainEvent.CurrentRecipeId, "RecipeChanged");
        }
    }

    private sealed class StubAlarmEventConsumer : IAlarmEventConsumer
    {
        public SchedulerContextUpdateResult Consume(AlarmRaisedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateRecoveryResult(domainEvent.AlarmId, null);
        }

        public SchedulerContextUpdateResult Consume(AlarmClearedEvent domainEvent, ISchedulerEventConsumptionContext context)
        {
            return CreateRecoveryResult(domainEvent.AlarmId, null);
        }
    }

    private sealed class StubMaterialContextUpdater : IMaterialContextUpdater
    {
        public SchedulerContextUpdateResult CreateUpdate(MaterialCreatedEvent domainEvent)
        {
            return CreateTrackingResult(domainEvent.MaterialId, null, null);
        }

        public SchedulerContextUpdateResult CreateUpdate(MaterialStateChangedEvent domainEvent)
        {
            return CreateTrackingResult(domainEvent.MaterialId, null, null);
        }

        public SchedulerContextUpdateResult CreateUpdate(MaterialLocationChangedEvent domainEvent)
        {
            return CreateTrackingResult(domainEvent.MaterialId, null, null);
        }

        public SchedulerContextUpdateResult CreateUpdate(SubstrateTrackedEvent domainEvent)
        {
            return new SchedulerContextUpdateResult(
                Array.Empty<RouteRefreshRequest>(),
                new[]
                {
                    new TrackingRefreshRequest(domainEvent.MaterialId, null, domainEvent.SubstrateId, null, null),
                },
                Array.Empty<RecoveryEvaluationRequest>(),
                Array.Empty<JobContextRefreshRequest>());
        }
    }

    private sealed class StubProductionJobContextUpdater : IProductionJobContextUpdater
    {
        public SchedulerContextUpdateResult CreateUpdate(ControlJobCreatedEvent domainEvent)
        {
            return CreateJobResult(domainEvent.ControlJobId, null, null, "ControlJobCreated");
        }

        public SchedulerContextUpdateResult CreateUpdate(ControlJobStateChangedEvent domainEvent)
        {
            return CreateJobResult(domainEvent.ControlJobId, null, null, "ControlJobStateChanged");
        }

        public SchedulerContextUpdateResult CreateUpdate(ProcessJobCreatedEvent domainEvent)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, domainEvent.RecipeId, "ProcessJobCreated");
        }

        public SchedulerContextUpdateResult CreateUpdate(ProcessJobStateChangedEvent domainEvent)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, null, "ProcessJobStateChanged");
        }

        public SchedulerContextUpdateResult CreateUpdate(RecipeAssignedEvent domainEvent)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, domainEvent.RecipeId, "RecipeAssigned");
        }

        public SchedulerContextUpdateResult CreateUpdate(RecipeChangedEvent domainEvent)
        {
            return CreateJobResult(null, domainEvent.ProcessJobId, domainEvent.CurrentRecipeId, "RecipeChanged");
        }
    }

    private sealed class StubRouteContextUpdater : IRouteContextUpdater
    {
        public SchedulerContextUpdateResult CreateUpdate(EquipmentStateChangedEvent domainEvent)
        {
            return CreateRouteResult(null, null, domainEvent.EquipmentId, "EquipmentStateChanged");
        }

        public SchedulerContextUpdateResult CreateUpdate(ControlStateChangedEvent domainEvent)
        {
            return CreateRouteResult(null, null, domainEvent.EquipmentId, "ControlStateChanged");
        }

        public SchedulerContextUpdateResult CreateUpdate(MaterialLocationChangedEvent domainEvent)
        {
            return CreateRouteResult(null, domainEvent.MaterialId, null, "MaterialLocationChanged");
        }

        public SchedulerContextUpdateResult CreateUpdate(CarrierLoadedEvent domainEvent)
        {
            return CreateRouteResult(null, null, domainEvent.EquipmentId, "CarrierLoaded");
        }

        public SchedulerContextUpdateResult CreateUpdate(CarrierUnloadedEvent domainEvent)
        {
            return CreateRouteResult(null, null, domainEvent.EquipmentId, "CarrierUnloaded");
        }

        public SchedulerContextUpdateResult CreateUpdate(ProcessJobCreatedEvent domainEvent)
        {
            return CreateRouteResult(domainEvent.ProcessJobId, null, null, "ProcessJobCreated");
        }

        public SchedulerContextUpdateResult CreateUpdate(ProcessJobStateChangedEvent domainEvent)
        {
            return CreateRouteResult(domainEvent.ProcessJobId, null, null, "ProcessJobStateChanged");
        }

        public SchedulerContextUpdateResult CreateUpdate(RecipeAssignedEvent domainEvent)
        {
            return CreateRouteResult(domainEvent.ProcessJobId, null, null, "RecipeAssigned");
        }
    }

    private sealed class StubRecoveryContextUpdater : IRecoveryContextUpdater
    {
        public SchedulerContextUpdateResult CreateUpdate(EquipmentStateChangedEvent domainEvent)
        {
            return CreateRecoveryResult(null, domainEvent.EquipmentId);
        }

        public SchedulerContextUpdateResult CreateUpdate(ControlStateChangedEvent domainEvent)
        {
            return CreateRecoveryResult(null, domainEvent.EquipmentId);
        }

        public SchedulerContextUpdateResult CreateUpdate(AlarmRaisedEvent domainEvent)
        {
            return CreateRecoveryResult(domainEvent.AlarmId, null);
        }

        public SchedulerContextUpdateResult CreateUpdate(AlarmClearedEvent domainEvent)
        {
            return CreateRecoveryResult(domainEvent.AlarmId, null);
        }
    }

    private static SchedulerContextUpdateResult CreateRouteResult(
        ProcessJobId? processJobId,
        MaterialId? materialId,
        EquipmentId? equipmentId,
        string reasonCode)
    {
        return new SchedulerContextUpdateResult(
            new[]
            {
                new RouteRefreshRequest(processJobId, materialId, equipmentId, reasonCode),
            },
            Array.Empty<TrackingRefreshRequest>(),
            Array.Empty<RecoveryEvaluationRequest>(),
            Array.Empty<JobContextRefreshRequest>());
    }

    private static SchedulerContextUpdateResult CreateTrackingResult(MaterialId materialId, ModuleId? moduleId, string? locationCode)
    {
        return new SchedulerContextUpdateResult(
            Array.Empty<RouteRefreshRequest>(),
            new[]
            {
                new TrackingRefreshRequest(materialId, null, null, moduleId, locationCode),
            },
            Array.Empty<RecoveryEvaluationRequest>(),
            Array.Empty<JobContextRefreshRequest>());
    }

    private static SchedulerContextUpdateResult CreateJobResult(
        ControlJobId? controlJobId,
        ProcessJobId? processJobId,
        RecipeId? recipeId,
        string reasonCode)
    {
        return new SchedulerContextUpdateResult(
            Array.Empty<RouteRefreshRequest>(),
            Array.Empty<TrackingRefreshRequest>(),
            Array.Empty<RecoveryEvaluationRequest>(),
            new[]
            {
                new JobContextRefreshRequest(controlJobId, processJobId, recipeId, reasonCode),
            });
    }

    private static SchedulerContextUpdateResult CreateRecoveryResult(AlarmId? alarmId, EquipmentId? equipmentId)
    {
        return new SchedulerContextUpdateResult(
            Array.Empty<RouteRefreshRequest>(),
            Array.Empty<TrackingRefreshRequest>(),
            new[]
            {
                new RecoveryEvaluationRequest(null, null, alarmId, equipmentId, RecoveryReason.OperatorAbort),
            },
            Array.Empty<JobContextRefreshRequest>());
    }
}
