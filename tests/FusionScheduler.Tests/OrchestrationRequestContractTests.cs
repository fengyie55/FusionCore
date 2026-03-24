using FusionDomain.ValueObjects;
using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationContracts;
using FusionScheduler.OrchestrationIntents;
using FusionScheduler.OrchestrationModels;
using FusionScheduler.OrchestrationRequests;
using FusionScheduler.Recovery;

namespace FusionScheduler.Tests;

public sealed class OrchestrationRequestContractTests
{
    [Fact]
    public void Orchestration_Request_Models_Can_Be_Instantiated()
    {
        var routeRefresh = new RouteRefreshRequest(
            new ProcessJobId("PJ-01"),
            new MaterialId("MAT-01"),
            new EquipmentId("EQ-01"),
            "RouteRefresh");
        var trackingRefresh = new TrackingRefreshRequest(
            new MaterialId("MAT-01"),
            new CarrierId("CAR-01"),
            new SubstrateId("SUB-01"),
            new ModuleId("BM-01"),
            "BM-01-SLOT-01");
        var jobRefresh = new JobContextRefreshRequest(
            new ControlJobId("CJ-01"),
            new ProcessJobId("PJ-01"),
            new RecipeId("RC-01"),
            "JobRefresh");
        var recoveryRefresh = new RecoveryEvaluationRequest(
            new ProcessJobId("PJ-01"),
            new MaterialId("MAT-01"),
            new AlarmId("AL-01"),
            new EquipmentId("EQ-01"),
            RecoveryReason.OperatorAbort);

        SchedulerOrchestrationRequest routeRequest = new RouteReevaluationRequest(
            "ORCH-001",
            OrchestrationPriority.High,
            OrchestrationRequestSource.DomainEventConsumption,
            DateTimeOffset.UtcNow,
            routeRefresh.ProcessJobId,
            routeRefresh.MaterialId,
            routeRefresh.EquipmentId,
            routeRefresh.ReasonCode,
            routeRefresh);
        SchedulerOrchestrationRequest materialRequest = new MaterialFlowReplanRequest(
            "ORCH-002",
            OrchestrationPriority.Normal,
            OrchestrationRequestSource.ContextRefresh,
            DateTimeOffset.UtcNow,
            trackingRefresh.MaterialId,
            trackingRefresh.CarrierId,
            trackingRefresh.SubstrateId,
            trackingRefresh.ModuleId,
            trackingRefresh.LocationCode,
            trackingRefresh);
        SchedulerOrchestrationRequest jobRequest = new JobProgressEvaluationRequest(
            "ORCH-003",
            OrchestrationPriority.Normal,
            OrchestrationRequestSource.DomainEventConsumption,
            DateTimeOffset.UtcNow,
            jobRefresh.ControlJobId,
            jobRefresh.ProcessJobId,
            jobRefresh.RecipeId,
            jobRefresh.ReasonCode,
            jobRefresh);
        SchedulerOrchestrationRequest recoveryRequest = new RecoveryAssessmentRequest(
            "ORCH-004",
            OrchestrationPriority.Critical,
            OrchestrationRequestSource.DomainEventConsumption,
            DateTimeOffset.UtcNow,
            recoveryRefresh.ProcessJobId,
            recoveryRefresh.MaterialId,
            recoveryRefresh.AlarmId,
            recoveryRefresh.EquipmentId,
            recoveryRefresh.SuggestedReason,
            recoveryRefresh);

        Assert.Equal(OrchestrationIntentType.RouteReevaluation, routeRequest.IntentType);
        Assert.Equal(OrchestrationIntentType.MaterialFlowReplan, materialRequest.IntentType);
        Assert.Equal(OrchestrationIntentType.JobProgressEvaluation, jobRequest.IntentType);
        Assert.Equal(OrchestrationIntentType.RecoveryAssessment, recoveryRequest.IntentType);
    }

    [Fact]
    public void Update_Result_Can_Be_Planned_Into_Orchestration_Requests()
    {
        var planner = new StubOrchestrationRequestPlanner();
        var inputContext = new OrchestrationInputContext(
            OrchestrationRequestSource.DomainEventConsumption,
            DateTimeOffset.UtcNow,
            "corr-01");
        var updateResult = new SchedulerContextUpdateResult(
            new[]
            {
                new RouteRefreshRequest(new ProcessJobId("PJ-01"), new MaterialId("MAT-01"), new EquipmentId("EQ-01"), "RouteRefresh"),
            },
            new[]
            {
                new TrackingRefreshRequest(new MaterialId("MAT-01"), null, null, new ModuleId("BM-01"), "BM-01-SLOT-01"),
            },
            new[]
            {
                new RecoveryEvaluationRequest(new ProcessJobId("PJ-01"), new MaterialId("MAT-01"), new AlarmId("AL-01"), new EquipmentId("EQ-01"), RecoveryReason.OperatorAbort),
            },
            new[]
            {
                new JobContextRefreshRequest(new ControlJobId("CJ-01"), new ProcessJobId("PJ-01"), new RecipeId("RC-01"), "JobRefresh"),
            });

        var requests = planner.Plan(updateResult, inputContext);

        Assert.Equal(4, requests.Count);
        Assert.Contains(requests, request => request is RouteReevaluationRequest);
        Assert.Contains(requests, request => request is MaterialFlowReplanRequest);
        Assert.Contains(requests, request => request is JobProgressEvaluationRequest);
        Assert.Contains(requests, request => request is RecoveryAssessmentRequest);
    }

    [Fact]
    public void Orchestrator_And_Gateway_Contracts_Can_Be_Implemented()
    {
        var planner = new StubOrchestrationRequestPlanner();
        var orchestrator = new StubContextRefreshOrchestrator(planner);
        var gateway = new StubSchedulerOrchestrationGateway();
        var updateResult = new SchedulerContextUpdateResult(
            new[]
            {
                new RouteRefreshRequest(new ProcessJobId("PJ-01"), null, new EquipmentId("EQ-01"), "RouteRefresh"),
            },
            Array.Empty<TrackingRefreshRequest>(),
            Array.Empty<RecoveryEvaluationRequest>(),
            Array.Empty<JobContextRefreshRequest>());
        var inputContext = new OrchestrationInputContext(
            OrchestrationRequestSource.DomainEventConsumption,
            DateTimeOffset.UtcNow,
            null);

        var requests = orchestrator.Orchestrate(updateResult, inputContext);
        gateway.Submit(requests);

        Assert.Single(gateway.SubmittedRequests);
        Assert.IsType<RouteReevaluationRequest>(gateway.SubmittedRequests[0]);
    }

    private sealed class StubOrchestrationRequestPlanner : IOrchestrationRequestPlanner
    {
        public IReadOnlyCollection<SchedulerOrchestrationRequest> Plan(
            SchedulerContextUpdateResult updateResult,
            OrchestrationInputContext inputContext)
        {
            var requests = new List<SchedulerOrchestrationRequest>();

            foreach (var routeRefresh in updateResult.RouteRefreshRequests)
            {
                requests.Add(new RouteReevaluationRequest(
                    CreateRequestId("route", requests.Count),
                    OrchestrationPriority.High,
                    inputContext.Source,
                    inputContext.RequestedAtUtc,
                    routeRefresh.ProcessJobId,
                    routeRefresh.MaterialId,
                    routeRefresh.EquipmentId,
                    routeRefresh.ReasonCode,
                    routeRefresh));
            }

            foreach (var trackingRefresh in updateResult.TrackingRefreshRequests)
            {
                requests.Add(new MaterialFlowReplanRequest(
                    CreateRequestId("tracking", requests.Count),
                    OrchestrationPriority.Normal,
                    inputContext.Source,
                    inputContext.RequestedAtUtc,
                    trackingRefresh.MaterialId,
                    trackingRefresh.CarrierId,
                    trackingRefresh.SubstrateId,
                    trackingRefresh.ModuleId,
                    trackingRefresh.LocationCode,
                    trackingRefresh));
            }

            foreach (var recoveryRefresh in updateResult.RecoveryEvaluationRequests)
            {
                requests.Add(new RecoveryAssessmentRequest(
                    CreateRequestId("recovery", requests.Count),
                    OrchestrationPriority.Critical,
                    inputContext.Source,
                    inputContext.RequestedAtUtc,
                    recoveryRefresh.ProcessJobId,
                    recoveryRefresh.MaterialId,
                    recoveryRefresh.AlarmId,
                    recoveryRefresh.EquipmentId,
                    recoveryRefresh.SuggestedReason,
                    recoveryRefresh));
            }

            foreach (var jobRefresh in updateResult.JobContextRefreshRequests)
            {
                requests.Add(new JobProgressEvaluationRequest(
                    CreateRequestId("job", requests.Count),
                    OrchestrationPriority.Normal,
                    inputContext.Source,
                    inputContext.RequestedAtUtc,
                    jobRefresh.ControlJobId,
                    jobRefresh.ProcessJobId,
                    jobRefresh.RecipeId,
                    jobRefresh.ReasonCode,
                    jobRefresh));
            }

            return requests;
        }

        private static string CreateRequestId(string prefix, int index)
        {
            return $"{prefix}-{index + 1:D3}";
        }
    }

    private sealed class StubContextRefreshOrchestrator : IContextRefreshOrchestrator
    {
        private readonly IOrchestrationRequestPlanner _planner;

        public StubContextRefreshOrchestrator(IOrchestrationRequestPlanner planner)
        {
            _planner = planner;
        }

        public IReadOnlyCollection<SchedulerOrchestrationRequest> Orchestrate(
            SchedulerContextUpdateResult updateResult,
            OrchestrationInputContext inputContext)
        {
            return _planner.Plan(updateResult, inputContext);
        }
    }

    private sealed class StubSchedulerOrchestrationGateway : ISchedulerOrchestrationGateway
    {
        public List<SchedulerOrchestrationRequest> SubmittedRequests { get; } = new();

        public void Submit(IReadOnlyCollection<SchedulerOrchestrationRequest> requests)
        {
            SubmittedRequests.AddRange(requests);
        }
    }
}
