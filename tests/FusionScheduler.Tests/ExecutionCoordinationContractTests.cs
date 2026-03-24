using FusionDomain.ValueObjects;
using FusionScheduler.CoordinationContracts;
using FusionScheduler.CoordinationInputs;
using FusionScheduler.CoordinationIntents;
using FusionScheduler.CoordinationResults;
using FusionScheduler.Models;
using FusionScheduler.PlanningIntents;
using FusionScheduler.PlanningResults;
using FusionScheduler.Recovery;

namespace FusionScheduler.Tests;

public sealed class ExecutionCoordinationContractTests
{
    [Fact]
    public void Coordination_Models_Can_Be_Instantiated()
    {
        var dispatchTask = new DispatchTask(
            "DT-01",
            new MaterialId("MAT-01"),
            new ModuleId("BM-01"),
            new ModuleId("PM-01"),
            null,
            "Transfer");
        var dispatchDecision = new DispatchPlanDecision(
            new[] { dispatchTask },
            PlanningConclusionKind.SkeletonReady,
            "DispatchReady");
        var routeDecision = new RoutePlanDecision(
            new RoutePlan(
                new ProcessJobId("PJ-01"),
                new EquipmentId("EQ-01"),
                new[] { dispatchTask }),
            PlanningConclusionKind.SkeletonReady,
            "RouteReady");
        var recoveryDecision = new RecoveryPlanDecision(
            new RecoveryPlan(
                new ProcessJobId("PJ-01"),
                RecoveryReason.OperatorAbort,
                new[] { RecoveryActionType.Hold }),
            PlanningConclusionKind.HoldForReview,
            "RecoveryReady");
        ExecutionCoordinationRequest request = new DispatchCoordinationInput(
            "COORD-REQ-01",
            "PLAN-01",
            CoordinationPriority.High,
            DateTimeOffset.UtcNow,
            dispatchDecision,
            dispatchDecision.DispatchTasks);
        var result = new CoordinationResult(
            "COORD-RES-01",
            request.CoordinationRequestId,
            request.IntentType,
            request.Priority,
            new CoordinationBasisReference("PLAN-01", "EV-01", PlanningConclusionKind.SkeletonReady),
            new CoordinationSummary(CoordinationOutcomeKind.ReadyForExecutionPreparation, "CoordReady", null),
            new CoordinationDecision(CoordinationOutcomeKind.ReadyForExecutionPreparation, "Proceed", true),
            new[] { new CoordinationConflict("None", null, null) },
            new[] { new CoordinationPrecheck("Prerequisite", true, null) });

        Assert.Equal(CoordinationIntentType.Dispatch, request.IntentType);
        Assert.Equal("COORD-RES-01", result.CoordinationResultId);
        Assert.True(result.Decision.CanProceedToExecutionPreparation);
        Assert.NotNull(routeDecision.RoutePlan);
        Assert.NotNull(recoveryDecision.RecoveryPlan);
    }

    [Fact]
    public void Plan_Result_Can_Be_Aligned_To_Coordination_Input()
    {
        var dispatchTask = new DispatchTask(
            "DT-01",
            new MaterialId("MAT-01"),
            new ModuleId("BM-01"),
            new ModuleId("PM-01"),
            null,
            "Transfer");
        var planResult = new SchedulingPlanResult(
            "PLAN-01",
            "EV-01",
            PlanningIntentType.Dispatch,
            PlanningPriority.High,
            new PlanningModels.PlanningBasisReference("EV-01", "ORCH-01", EvaluationIntents.EvaluationConclusionKind.NeedFurtherPlanning),
            new PlanningSummary(PlanningConclusionKind.SkeletonReady, "PlanReady", null),
            new DispatchPlanDecision(new[] { dispatchTask }, PlanningConclusionKind.SkeletonReady, "DispatchReady"),
            null,
            null);
        ExecutionCoordinationRequest request = new DispatchCoordinationInput(
            "COORD-REQ-01",
            planResult.PlanResultId,
            planResult.Priority.ToCoordinationPriority(),
            DateTimeOffset.UtcNow,
            planResult.DispatchPlanDecision!,
            planResult.DispatchPlanDecision!.DispatchTasks);

        Assert.Equal(planResult.PlanResultId, request.PlanResultId);
        Assert.Equal(CoordinationIntentType.Dispatch, request.IntentType);
        Assert.Equal(CoordinationPriority.High, request.Priority);
    }

    [Fact]
    public void Coordination_Contracts_Can_Be_Implemented_With_Minimal_Stubs()
    {
        ICoordinationContextReader contextReader = new StubCoordinationContextReader();
        ICoordinationResultAggregator aggregator = new StubCoordinationResultAggregator();
        IPlanCoordinationService service = new StubPlanCoordinationService(aggregator);
        IExecutionCoordinationGateway gateway = new StubExecutionCoordinationGateway();
        var planResult = new SchedulingPlanResult(
            "PLAN-REC-01",
            "EV-REC-01",
            PlanningIntentType.Recovery,
            PlanningPriority.Critical,
            new PlanningModels.PlanningBasisReference("EV-REC-01", "ORCH-REC-01", EvaluationIntents.EvaluationConclusionKind.HoldForReview),
            new PlanningSummary(PlanningConclusionKind.HoldForReview, "RecoveryPlanReady", null),
            null,
            null,
            new RecoveryPlanDecision(
                new RecoveryPlan(
                    new ProcessJobId("PJ-01"),
                    RecoveryReason.OperatorAbort,
                    new[] { RecoveryActionType.Hold }),
                PlanningConclusionKind.HoldForReview,
                "RecoveryReady"));
        var request = contextReader.ReadFor(planResult);
        gateway.Submit(new[] { request });
        var results = service.Coordinate(
            new[] { request },
            new CoordinationInputContext("corr-01", DateTimeOffset.UtcNow, "coord-v1", CoordinationPriority.Critical));
        var result = Assert.Single(results);

        Assert.Single(((StubExecutionCoordinationGateway)gateway).SubmittedRequests);
        Assert.Equal(CoordinationIntentType.Recovery, result.IntentType);
        Assert.Equal(CoordinationOutcomeKind.HoldForReview, result.Summary.OutcomeKind);
    }

    private sealed class StubCoordinationContextReader : ICoordinationContextReader
    {
        public ExecutionCoordinationRequest ReadFor(SchedulingPlanResult planResult)
        {
            return planResult.IntentType switch
            {
                PlanningIntentType.Dispatch => new DispatchCoordinationInput(
                    $"coord-{planResult.PlanResultId}",
                    planResult.PlanResultId,
                    planResult.Priority.ToCoordinationPriority(),
                    DateTimeOffset.UtcNow,
                    planResult.DispatchPlanDecision!,
                    planResult.DispatchPlanDecision!.DispatchTasks),
                PlanningIntentType.Route => new RouteCoordinationInput(
                    $"coord-{planResult.PlanResultId}",
                    planResult.PlanResultId,
                    planResult.Priority.ToCoordinationPriority(),
                    DateTimeOffset.UtcNow,
                    planResult.RoutePlanDecision!,
                    planResult.RoutePlanDecision!.RoutePlan),
                PlanningIntentType.Recovery => new RecoveryCoordinationInput(
                    $"coord-{planResult.PlanResultId}",
                    planResult.PlanResultId,
                    planResult.Priority.ToCoordinationPriority(),
                    DateTimeOffset.UtcNow,
                    planResult.RecoveryPlanDecision!,
                    planResult.RecoveryPlanDecision!.RecoveryPlan),
                _ => new DispatchCoordinationInput(
                    $"coord-{planResult.PlanResultId}",
                    planResult.PlanResultId,
                    planResult.Priority.ToCoordinationPriority(),
                    DateTimeOffset.UtcNow,
                    new DispatchPlanDecision(Array.Empty<DispatchTask>(), PlanningConclusionKind.Unknown, "Unknown"),
                    Array.Empty<DispatchTask>()),
            };
        }
    }

    private sealed class StubCoordinationResultAggregator : ICoordinationResultAggregator
    {
        public IReadOnlyCollection<CoordinationResult> Aggregate(IReadOnlyCollection<CoordinationResult> results)
        {
            return results;
        }
    }

    private sealed class StubPlanCoordinationService : IPlanCoordinationService
    {
        private readonly ICoordinationResultAggregator _aggregator;

        public StubPlanCoordinationService(ICoordinationResultAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        public IReadOnlyCollection<CoordinationResult> Coordinate(
            IReadOnlyCollection<ExecutionCoordinationRequest> requests,
            CoordinationInputContext inputContext)
        {
            var results = requests
                .Select(request => new CoordinationResult(
                    $"res-{request.CoordinationRequestId}",
                    request.CoordinationRequestId,
                    request.IntentType,
                    inputContext.Priority,
                    new CoordinationBasisReference(request.PlanResultId, "EV-UNKNOWN", PlanningConclusionKind.Unknown),
                    new CoordinationSummary(
                        request.IntentType == CoordinationIntentType.Recovery
                            ? CoordinationOutcomeKind.HoldForReview
                            : CoordinationOutcomeKind.ReadyForExecutionPreparation,
                        "CoordinationReady",
                        null),
                    new CoordinationDecision(
                        request.IntentType == CoordinationIntentType.Recovery
                            ? CoordinationOutcomeKind.HoldForReview
                            : CoordinationOutcomeKind.ReadyForExecutionPreparation,
                        "PreExecutionCheck",
                        request.IntentType != CoordinationIntentType.Recovery),
                    Array.Empty<CoordinationConflict>(),
                    new[]
                    {
                        new CoordinationPrecheck("BaseCheck", true, null),
                    }))
                .ToArray();

            return _aggregator.Aggregate(results);
        }
    }

    private sealed class StubExecutionCoordinationGateway : IExecutionCoordinationGateway
    {
        public List<ExecutionCoordinationRequest> SubmittedRequests { get; } = new();

        public void Submit(IReadOnlyCollection<ExecutionCoordinationRequest> requests)
        {
            SubmittedRequests.AddRange(requests);
        }
    }
}
