using FusionDomain.ValueObjects;
using FusionScheduler.EvaluationContracts;
using FusionScheduler.EvaluationIntents;
using FusionScheduler.EvaluationModels;
using FusionScheduler.EvaluationResults;
using FusionScheduler.EventModels;
using FusionScheduler.OrchestrationIntents;
using FusionScheduler.OrchestrationRequests;
using FusionScheduler.Recovery;

namespace FusionScheduler.Tests;

public sealed class SchedulingEvaluationContractTests
{
    [Fact]
    public void Evaluation_Result_Models_Can_Be_Instantiated()
    {
        var summary = new EvaluationSummary(
            EvaluationConclusionKind.NeedFurtherPlanning,
            "RouteReview",
            "需要继续规划。");
        var routeDecision = new RouteDecision(
            new ProcessJobId("PJ-01"),
            new MaterialId("MAT-01"),
            new EquipmentId("EQ-01"),
            EvaluationConclusionKind.NeedFurtherPlanning,
            "RoutePending");
        var materialDecision = new MaterialFlowDecision(
            new MaterialId("MAT-01"),
            new CarrierId("CAR-01"),
            new SubstrateId("SUB-01"),
            new ModuleId("BM-01"),
            EvaluationConclusionKind.NeedFollowUpEvaluation,
            "MaterialPending");
        var jobDecision = new JobProgressDecision(
            new ControlJobId("CJ-01"),
            new ProcessJobId("PJ-01"),
            new RecipeId("RC-01"),
            EvaluationConclusionKind.NoFurtherAction,
            "JobStable");
        var recoveryDecision = new RecoveryDecision(
            new ProcessJobId("PJ-01"),
            new MaterialId("MAT-01"),
            new AlarmId("AL-01"),
            new EquipmentId("EQ-01"),
            RecoveryReason.OperatorAbort,
            EvaluationConclusionKind.HoldForReview,
            "RecoveryHold");
        var result = new SchedulingEvaluationResult(
            "EV-001",
            "ORCH-001",
            EvaluationIntentType.Route,
            EvaluationPriority.High,
            summary,
            routeDecision,
            materialDecision,
            jobDecision,
            recoveryDecision);

        Assert.Equal("EV-001", result.ResultId);
        Assert.Equal(EvaluationIntentType.Route, result.IntentType);
        Assert.Equal(EvaluationConclusionKind.NeedFurtherPlanning, result.Summary.ConclusionKind);
        Assert.NotNull(result.RouteDecision);
    }

    [Fact]
    public void Orchestration_Request_And_Evaluation_Result_Can_Be_Aligned()
    {
        SchedulerOrchestrationRequest request = new RouteReevaluationRequest(
            "ORCH-001",
            OrchestrationPriority.High,
            OrchestrationRequestSource.DomainEventConsumption,
            DateTimeOffset.UtcNow,
            new ProcessJobId("PJ-01"),
            new MaterialId("MAT-01"),
            new EquipmentId("EQ-01"),
            "RouteRefresh",
            new RouteRefreshRequest(
                new ProcessJobId("PJ-01"),
                new MaterialId("MAT-01"),
                new EquipmentId("EQ-01"),
                "RouteRefresh"));
        var inputContext = new EvaluationInputContext(
            "corr-01",
            request.Source,
            DateTimeOffset.UtcNow,
            "ctx-v1",
            request.Priority.ToEvaluationPriority());
        var result = new SchedulingEvaluationResult(
            "EV-001",
            request.RequestId,
            request.IntentType.ToEvaluationIntentType(),
            inputContext.Priority,
            new EvaluationSummary(EvaluationConclusionKind.NeedFurtherPlanning, "RouteReview", null),
            new RouteDecision(
                new ProcessJobId("PJ-01"),
                new MaterialId("MAT-01"),
                new EquipmentId("EQ-01"),
                EvaluationConclusionKind.NeedFurtherPlanning,
                "RoutePending"),
            null,
            null,
            null);

        Assert.Equal(request.RequestId, result.RequestId);
        Assert.Equal(EvaluationIntentType.Route, result.IntentType);
        Assert.Equal(EvaluationPriority.High, result.Priority);
    }

    [Fact]
    public void Evaluation_Contracts_Can_Be_Implemented_With_Minimal_Stubs()
    {
        IEvaluationContextReader contextReader = new StubEvaluationContextReader();
        IOrchestrationRequestEvaluator evaluator = new StubOrchestrationRequestEvaluator();
        IEvaluationResultCoordinator coordinator = new StubEvaluationResultCoordinator();
        ISchedulingEvaluationService service = new StubSchedulingEvaluationService(contextReader, evaluator, coordinator);
        var requests = new SchedulerOrchestrationRequest[]
        {
            new RecoveryAssessmentRequest(
                "ORCH-REC-001",
                OrchestrationPriority.Critical,
                OrchestrationRequestSource.ContextRefresh,
                DateTimeOffset.UtcNow,
                new ProcessJobId("PJ-01"),
                new MaterialId("MAT-01"),
                new AlarmId("AL-01"),
                new EquipmentId("EQ-01"),
                RecoveryReason.OperatorAbort,
                new RecoveryEvaluationRequest(
                    new ProcessJobId("PJ-01"),
                    new MaterialId("MAT-01"),
                    new AlarmId("AL-01"),
                    new EquipmentId("EQ-01"),
                    RecoveryReason.OperatorAbort)),
        };
        var inputContext = new EvaluationInputContext(
            "corr-02",
            OrchestrationRequestSource.ContextRefresh,
            DateTimeOffset.UtcNow,
            "ctx-v2",
            EvaluationPriority.Critical);

        var results = service.Evaluate(requests, inputContext);
        var result = Assert.Single(results);

        Assert.Equal(EvaluationIntentType.Recovery, result.IntentType);
        Assert.Equal(EvaluationConclusionKind.HoldForReview, result.Summary.ConclusionKind);
    }

    private sealed class StubEvaluationContextReader : IEvaluationContextReader
    {
        public EvaluationInputContext ReadFor(SchedulerOrchestrationRequest request)
        {
            return new EvaluationInputContext(
                request.RequestId,
                request.Source,
                request.RequestedAtUtc,
                "ctx-read",
                request.Priority.ToEvaluationPriority());
        }
    }

    private sealed class StubOrchestrationRequestEvaluator : IOrchestrationRequestEvaluator
    {
        public SchedulingEvaluationResult Evaluate(
            SchedulerOrchestrationRequest request,
            EvaluationInputContext inputContext)
        {
            return request switch
            {
                RouteReevaluationRequest routeRequest => new SchedulingEvaluationResult(
                    CreateResultId(routeRequest.RequestId),
                    routeRequest.RequestId,
                    EvaluationIntentType.Route,
                    inputContext.Priority,
                    new EvaluationSummary(EvaluationConclusionKind.NeedFurtherPlanning, routeRequest.ReasonCode, null),
                    new RouteDecision(
                        routeRequest.ProcessJobId,
                        routeRequest.MaterialId,
                        routeRequest.EquipmentId,
                        EvaluationConclusionKind.NeedFurtherPlanning,
                        "RoutePending"),
                    null,
                    null,
                    null),
                MaterialFlowReplanRequest materialRequest => new SchedulingEvaluationResult(
                    CreateResultId(materialRequest.RequestId),
                    materialRequest.RequestId,
                    EvaluationIntentType.MaterialFlow,
                    inputContext.Priority,
                    new EvaluationSummary(EvaluationConclusionKind.NeedFollowUpEvaluation, "MaterialFlowReview", null),
                    null,
                    new MaterialFlowDecision(
                        materialRequest.MaterialId,
                        materialRequest.CarrierId,
                        materialRequest.SubstrateId,
                        materialRequest.ModuleId,
                        EvaluationConclusionKind.NeedFollowUpEvaluation,
                        "MaterialPending"),
                    null,
                    null),
                JobProgressEvaluationRequest jobRequest => new SchedulingEvaluationResult(
                    CreateResultId(jobRequest.RequestId),
                    jobRequest.RequestId,
                    EvaluationIntentType.JobProgress,
                    inputContext.Priority,
                    new EvaluationSummary(EvaluationConclusionKind.NoFurtherAction, jobRequest.ReasonCode, null),
                    null,
                    null,
                    new JobProgressDecision(
                        jobRequest.ControlJobId,
                        jobRequest.ProcessJobId,
                        jobRequest.RecipeId,
                        EvaluationConclusionKind.NoFurtherAction,
                        "JobStable"),
                    null),
                RecoveryAssessmentRequest recoveryRequest => new SchedulingEvaluationResult(
                    CreateResultId(recoveryRequest.RequestId),
                    recoveryRequest.RequestId,
                    EvaluationIntentType.Recovery,
                    inputContext.Priority,
                    new EvaluationSummary(EvaluationConclusionKind.HoldForReview, "RecoveryReview", null),
                    null,
                    null,
                    null,
                    new RecoveryDecision(
                        recoveryRequest.ProcessJobId,
                        recoveryRequest.MaterialId,
                        recoveryRequest.AlarmId,
                        recoveryRequest.EquipmentId,
                        recoveryRequest.SuggestedReason,
                        EvaluationConclusionKind.HoldForReview,
                        "RecoveryHold")),
                _ => new SchedulingEvaluationResult(
                    CreateResultId(request.RequestId),
                    request.RequestId,
                    EvaluationIntentType.Unknown,
                    inputContext.Priority,
                    new EvaluationSummary(EvaluationConclusionKind.Unknown, "Unknown", null),
                    null,
                    null,
                    null,
                    null),
            };
        }

        private static string CreateResultId(string requestId)
        {
            return $"eval-{requestId}";
        }
    }

    private sealed class StubEvaluationResultCoordinator : IEvaluationResultCoordinator
    {
        public IReadOnlyCollection<SchedulingEvaluationResult> Coordinate(
            IReadOnlyCollection<SchedulingEvaluationResult> results)
        {
            return results;
        }
    }

    private sealed class StubSchedulingEvaluationService : ISchedulingEvaluationService
    {
        private readonly IEvaluationContextReader _contextReader;
        private readonly IOrchestrationRequestEvaluator _evaluator;
        private readonly IEvaluationResultCoordinator _coordinator;

        public StubSchedulingEvaluationService(
            IEvaluationContextReader contextReader,
            IOrchestrationRequestEvaluator evaluator,
            IEvaluationResultCoordinator coordinator)
        {
            _contextReader = contextReader;
            _evaluator = evaluator;
            _coordinator = coordinator;
        }

        public IReadOnlyCollection<SchedulingEvaluationResult> Evaluate(
            IReadOnlyCollection<SchedulerOrchestrationRequest> requests,
            EvaluationInputContext inputContext)
        {
            var results = requests
                .Select(request =>
                {
                    var context = _contextReader.ReadFor(request);
                    var mergedContext = context with
                    {
                        CorrelationId = inputContext.CorrelationId ?? context.CorrelationId,
                        EvaluatedAtUtc = inputContext.EvaluatedAtUtc,
                        ContextVersion = inputContext.ContextVersion ?? context.ContextVersion,
                        Priority = inputContext.Priority,
                    };

                    return _evaluator.Evaluate(request, mergedContext);
                })
                .ToArray();

            return _coordinator.Coordinate(results);
        }
    }
}
