using FusionDomain.Aggregates;
using FusionDomain.Entities;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;
using FusionEquipment.Abstractions.Context;
using FusionEquipment.Abstractions.Contracts;
using FusionEquipment.Abstractions.Enums;
using FusionScheduler.Commands;
using FusionScheduler.Common;
using FusionScheduler.Contracts;
using FusionScheduler.Events;
using FusionScheduler.Models;
using FusionScheduler.Policies;
using FusionScheduler.Queries;
using FusionScheduler.Recovery;

namespace FusionScheduler.Tests;

public sealed class SchedulerContractSkeletonTests
{
    [Fact]
    public void Core_Models_Can_Be_Instantiated()
    {
        var equipmentId = new EquipmentId("EQ-01");
        var controlJob = new ControlJob(new ControlJobId("CJ-01"), ControlState.Created);
        var processJob = new ProcessJob(new ProcessJobId("PJ-01"), ControlState.Queued, new RecipeId("RC-01"));
        var jobContext = new ProductionJobContext(equipmentId, controlJob, processJob, new RecipeId("RC-01"));
        var material = new Material(new MaterialId("MAT-01"), MaterialState.Waiting);
        var module = new StubModule();
        var materialContext = new MaterialContext(material.Id, material, module.ModuleId, "BM-01-SLOT-01", module);
        var dispatchTask = new DispatchTask("DT-01", material.Id, null, module.ModuleId, module, "Transfer");
        var routePlan = new RoutePlan(processJob.Id, equipmentId, new[] { dispatchTask });
        var recoveryPlan = new RecoveryPlan(processJob.Id, RecoveryReason.OperatorAbort, new[] { RecoveryActionType.Hold });

        Assert.Equal("EQ-01", jobContext.EquipmentId.Value);
        Assert.Equal("MAT-01", materialContext.MaterialId.Value);
        Assert.Single(routePlan.Tasks);
        Assert.Single(recoveryPlan.Actions);
    }

    [Fact]
    public void Commands_Queries_And_Events_Can_Be_Created()
    {
        var createCommand = new CreateControlJobCommand(
            new EquipmentId("EQ-01"),
            new ControlJobId("CJ-01"),
            new ProcessJobId("PJ-01"),
            new RecipeId("RC-01"));
        var startCommand = new StartSchedulingCommand(new EquipmentId("EQ-01"), new ProcessJobId("PJ-01"));
        var abortCommand = new AbortMaterialFlowCommand(new ProcessJobId("PJ-01"), new MaterialId("MAT-01"), RecoveryReason.OperatorAbort);
        var unloadCommand = new RequestMaterialUnloadCommand(new ProcessJobId("PJ-01"), new MaterialId("MAT-01"), RecoveryReason.MaterialUnloadRequested);
        var locationQuery = new GetMaterialLocationQuery(new MaterialId("MAT-01"));
        var routeQuery = new GetRoutePlanQuery(new ProcessJobId("PJ-01"));
        var statusQuery = new GetJobStatusQuery(new ControlJobId("CJ-01"));
        var dispatchedEvent = new MaterialDispatchedEvent(new MaterialId("MAT-01"), "DT-01", new ModuleId("TM-01"));
        var arrivedEvent = new MaterialArrivedEvent(new MaterialId("MAT-01"), new ModuleId("PM-01"), "PM-01");
        var plannedEvent = new ProcessRoutePlannedEvent(new ProcessJobId("PJ-01"), 1);
        var recoveryEvent = new RecoveryPlanGeneratedEvent(new ProcessJobId("PJ-01"), RecoveryReason.OperatorAbort, 1);

        Assert.Equal("CJ-01", createCommand.ControlJobId.Value);
        Assert.Equal("PJ-01", startCommand.ProcessJobId.Value);
        Assert.Equal(RecoveryReason.OperatorAbort, abortCommand.Reason);
        Assert.Equal(RecoveryReason.MaterialUnloadRequested, unloadCommand.Reason);
        Assert.Equal("MAT-01", locationQuery.MaterialId.Value);
        Assert.Equal("PJ-01", routeQuery.ProcessJobId.Value);
        Assert.Equal("CJ-01", statusQuery.ControlJobId.Value);
        Assert.Equal("DT-01", dispatchedEvent.DispatchTaskId);
        Assert.Equal("PM-01", arrivedEvent.Location);
        Assert.Equal(1, plannedEvent.DispatchTaskCount);
        Assert.Equal(1, recoveryEvent.ActionCount);
    }

    [Fact]
    public void Interfaces_Can_Be_Implemented_With_Minimal_Stubs()
    {
        var service = new StubSchedulerService();
        var planner = new StubRoutePlanner();
        var tracker = new StubMaterialTracker();
        var recoveryPlanner = new StubRecoveryPlanner();
        var contextReader = new StubSchedulerContextReader();

        var jobContext = service.CreateControlJob(
            new CreateControlJobCommand(
                new EquipmentId("EQ-01"),
                new ControlJobId("CJ-01"),
                new ProcessJobId("PJ-01"),
                new RecipeId("RC-01")));
        var routePlan = planner.PlanRoute(jobContext, new StubRoutingPolicy());
        var materialContext = tracker.GetMaterialContext(new MaterialId("MAT-01"));
        var recoveryPlan = recoveryPlanner.CreateRecoveryPlan(jobContext, RecoveryReason.OperatorAbort, new StubRecoveryPolicy());
        var status = contextReader.GetJobStatus(new ControlJobId("CJ-01"));

        Assert.Equal("PJ-01", routePlan.ProcessJobId.Value);
        Assert.Equal("MAT-01", materialContext.MaterialId.Value);
        Assert.Equal(RecoveryReason.OperatorAbort, recoveryPlan.Reason);
        Assert.Equal(ControlState.Active, status.State);
    }

    private sealed class StubModule : IEquipmentModule
    {
        public EquipmentId EquipmentId { get; } = new("EQ-01");

        public ModuleId ModuleId { get; } = new("TM-01");

        public string Name { get; } = "Transfer Module";

        public ModuleType ModuleType { get; } = ModuleType.Transfer;

        public ModuleContext Context { get; } = new(new EquipmentId("EQ-01"), new ModuleId("TM-01"), "Transfer Module");

        public IReadOnlyCollection<ModuleCapability> Capabilities { get; } = new[] { ModuleCapability.Transfer };

        public bool IsEnabled { get; } = true;
    }

    private sealed class StubSchedulerService : ISchedulerService
    {
        public ProductionJobContext CreateControlJob(CreateControlJobCommand command)
        {
            return new ProductionJobContext(
                command.EquipmentId,
                new ControlJob(command.ControlJobId, ControlState.Created),
                new ProcessJob(command.ProcessJobId, ControlState.Created, command.RecipeId),
                command.RecipeId);
        }

        public RoutePlan StartScheduling(StartSchedulingCommand command)
        {
            return new RoutePlan(command.ProcessJobId, command.EquipmentId, Array.Empty<DispatchTask>());
        }

        public RecoveryPlan AbortMaterialFlow(AbortMaterialFlowCommand command)
        {
            return new RecoveryPlan(command.ProcessJobId, command.Reason, new[] { RecoveryActionType.Hold });
        }

        public RecoveryPlan RequestMaterialUnload(RequestMaterialUnloadCommand command)
        {
            return new RecoveryPlan(command.ProcessJobId, command.Reason, new[] { RecoveryActionType.Unload });
        }

        public JobStatusView GetJobStatus(ProductionJobContext jobContext)
        {
            return new JobStatusView(jobContext.ControlJob.Id, jobContext.ProcessJob.Id, ControlState.Active, "Active");
        }
    }

    private sealed class StubRoutePlanner : IRoutePlanner
    {
        public RoutePlan PlanRoute(ProductionJobContext jobContext, IRoutingPolicy routingPolicy)
        {
            return new RoutePlan(jobContext.ProcessJob.Id, jobContext.EquipmentId, Array.Empty<DispatchTask>());
        }
    }

    private sealed class StubMaterialTracker : IMaterialTracker
    {
        public MaterialContext GetMaterialContext(MaterialId materialId)
        {
            var material = new Material(materialId, MaterialState.Waiting);
            return new MaterialContext(materialId, material, null, "BUFFER-01", null);
        }
    }

    private sealed class StubRecoveryPlanner : IRecoveryPlanner
    {
        public RecoveryPlan CreateRecoveryPlan(ProductionJobContext jobContext, RecoveryReason reason, IRecoveryPolicy recoveryPolicy)
        {
            return new RecoveryPlan(jobContext.ProcessJob.Id, reason, new[] { RecoveryActionType.Hold });
        }
    }

    private sealed class StubSchedulerContextReader : ISchedulerContextReader
    {
        public JobStatusView GetJobStatus(ControlJobId controlJobId)
        {
            return new JobStatusView(controlJobId, new ProcessJobId("PJ-01"), ControlState.Active, "Active");
        }

        public RoutePlan? GetRoutePlan(ProcessJobId processJobId)
        {
            return new RoutePlan(processJobId, new EquipmentId("EQ-01"), Array.Empty<DispatchTask>());
        }

        public MaterialContext? GetMaterialContext(MaterialId materialId)
        {
            var material = new Material(materialId, MaterialState.Waiting);
            return new MaterialContext(materialId, material, null, "BUFFER-01", null);
        }
    }

    private sealed class StubRoutingPolicy : IRoutingPolicy
    {
        public string Name => "Default";

        public bool AppliesTo(ProductionJobContext jobContext) => true;
    }

    private sealed class StubRecoveryPolicy : IRecoveryPolicy
    {
        public string Name => "Default";

        public bool AppliesTo(ProductionJobContext jobContext, RecoveryReason reason) => true;
    }
}
