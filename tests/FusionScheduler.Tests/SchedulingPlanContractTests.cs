using FusionDomain.ValueObjects;
using FusionScheduler.EvaluationIntents;
using FusionScheduler.EvaluationModels;
using FusionScheduler.EvaluationResults;
using FusionScheduler.Models;
using FusionScheduler.PlanningContracts;
using FusionScheduler.PlanningIntents;
using FusionScheduler.PlanningModels;
using FusionScheduler.PlanningResults;
using FusionScheduler.Recovery;

namespace FusionScheduler.Tests;

public sealed class SchedulingPlanContractTests
{
    [Fact]
    public void Plan_Result_Models_Can_Be_Instantiated()
    {
        var dispatchTask = new DispatchTask(
            "DT-01",
            new MaterialId("MAT-01"),
            new ModuleId("BM-01"),
            new ModuleId("PM-01"),
            null,
            "Transfer");
        var routePlan = new RoutePlan(
            new ProcessJobId("PJ-01"),
            new EquipmentId("EQ-01"),
            new[] { dispatchTask });
        var recoveryPlan = new RecoveryPlan(
            new ProcessJobId("PJ-01"),
            RecoveryReason.OperatorAbort,
            new[] { RecoveryActionType.Hold });
        var basis = new PlanningBasisReference(
            "EV-01",
            "ORCH-01",
            EvaluationConclusionKind.NeedFurtherPlanning);
        var summary = new PlanningSummary(
            PlanningConclusionKind.SkeletonReady,
            "PlanReady",
            "已形成最小计划骨架。");
        var result = new SchedulingPlanResult(
            "PLAN-01",
            "EV-01",
            PlanningIntentType.Route,
            PlanningPriority.High,
            basis,
            summary,
            new DispatchPlanDecision(new[] { dispatchTask }, PlanningConclusionKind.SkeletonReady, "DispatchReady"),
            new RoutePlanDecision(routePlan, PlanningConclusionKind.SkeletonReady, "RouteReady"),
            new RecoveryPlanDecision(recoveryPlan, PlanningConclusionKind.NeedCoordination, "RecoveryReserved"));

        Assert.Equal("PLAN-01", result.PlanResultId);
        Assert.Equal(PlanningIntentType.Route, result.IntentType);
        Assert.NotNull(result.RoutePlanDecision);
        Assert.Single(result.DispatchPlanDecision!.DispatchTasks);
    }

    [Fact]
    public void Evaluation_Result_Can_Be_Aligned_To_Plan_Result()
    {
        var evaluationResult = new SchedulingEvaluationResult(
            "EV-01",
            "ORCH-01",
            EvaluationIntentType.Route,
            EvaluationPriority.High,
            new EvaluationSummary(
                EvaluationConclusionKind.NeedFurtherPlanning,
                "RouteReview",
                null),
            new RouteDecision(
                new ProcessJobId("PJ-01"),
                new MaterialId("MAT-01"),
                new EquipmentId("EQ-01"),
                EvaluationConclusionKind.NeedFurtherPlanning,
                "RoutePending"),
            null,
            null,
            null);
        var inputContext = new PlanningInputContext(
            "corr-01",
            DateTimeOffset.UtcNow,
            "plan-v1",
            evaluationResult.Priority.ToPlanningPriority());
        var result = new SchedulingPlanResult(
            "PLAN-01",
            evaluationResult.ResultId,
            evaluationResult.IntentType.ToPlanningIntentType(),
            inputContext.Priority,
            new PlanningBasisReference(
                evaluationResult.ResultId,
                evaluationResult.RequestId,
                evaluationResult.Summary.ConclusionKind),
            new PlanningSummary(
                evaluationResult.Summary.ConclusionKind.ToPlanningConclusionKind(),
                "RoutePlanReady",
                null),
            null,
            new RoutePlanDecision(
                new RoutePlan(
                    new ProcessJobId("PJ-01"),
                    new EquipmentId("EQ-01"),
                    Array.Empty<DispatchTask>()),
                PlanningConclusionKind.SkeletonReady,
                "RouteReady"),
            null);

        Assert.Equal(evaluationResult.ResultId, result.EvaluationResultId);
        Assert.Equal(PlanningIntentType.Route, result.IntentType);
        Assert.Equal(PlanningPriority.High, result.Priority);
    }

    [Fact]
    public void Planning_Contracts_Can_Be_Implemented_With_Minimal_Stubs()
    {
        IPlanningContextReader contextReader = new StubPlanningContextReader();
        IEvaluationResultPlanner planner = new StubEvaluationResultPlanner();
        IPlanningResultCoordinator coordinator = new StubPlanningResultCoordinator();
        ISchedulingPlanBuilder builder = new StubSchedulingPlanBuilder(contextReader, planner, coordinator);
        var evaluationResults = new[]
        {
            new SchedulingEvaluationResult(
                "EV-REC-01",
                "ORCH-REC-01",
                EvaluationIntentType.Recovery,
                EvaluationPriority.Critical,
                new EvaluationSummary(
                    EvaluationConclusionKind.HoldForReview,
                    "RecoveryReview",
                    null),
                null,
                null,
                null,
                new RecoveryDecision(
                    new ProcessJobId("PJ-01"),
                    new MaterialId("MAT-01"),
                    new AlarmId("AL-01"),
                    new EquipmentId("EQ-01"),
                    RecoveryReason.OperatorAbort,
                    EvaluationConclusionKind.HoldForReview,
                    "RecoveryHold")),
        };
        var inputContext = new PlanningInputContext(
            "corr-02",
            DateTimeOffset.UtcNow,
            "plan-v2",
            PlanningPriority.Critical);

        var results = builder.Build(evaluationResults, inputContext);
        var result = Assert.Single(results);

        Assert.Equal(PlanningIntentType.Recovery, result.IntentType);
        Assert.Equal(PlanningConclusionKind.HoldForReview, result.Summary.ConclusionKind);
        Assert.NotNull(result.RecoveryPlanDecision);
    }

    private sealed class StubPlanningContextReader : IPlanningContextReader
    {
        public PlanningInputContext ReadFor(SchedulingEvaluationResult evaluationResult)
        {
            return new PlanningInputContext(
                evaluationResult.RequestId,
                DateTimeOffset.UtcNow,
                "plan-read",
                evaluationResult.Priority.ToPlanningPriority());
        }
    }

    private sealed class StubEvaluationResultPlanner : IEvaluationResultPlanner
    {
        public SchedulingPlanResult Plan(
            SchedulingEvaluationResult evaluationResult,
            PlanningInputContext inputContext)
        {
            var basis = new PlanningBasisReference(
                evaluationResult.ResultId,
                evaluationResult.RequestId,
                evaluationResult.Summary.ConclusionKind);
            var summary = new PlanningSummary(
                evaluationResult.Summary.ConclusionKind.ToPlanningConclusionKind(),
                evaluationResult.Summary.SummaryCode,
                evaluationResult.Summary.Notes);

            return evaluationResult.IntentType switch
            {
                EvaluationIntentType.Route => new SchedulingPlanResult(
                    CreatePlanResultId(evaluationResult.ResultId),
                    evaluationResult.ResultId,
                    PlanningIntentType.Route,
                    inputContext.Priority,
                    basis,
                    summary,
                    null,
                    new RoutePlanDecision(
                        new RoutePlan(
                            evaluationResult.RouteDecision?.ProcessJobId ?? new ProcessJobId("PJ-UNKNOWN"),
                            evaluationResult.RouteDecision?.EquipmentId ?? new EquipmentId("EQ-UNKNOWN"),
                            Array.Empty<DispatchTask>()),
                        PlanningConclusionKind.SkeletonReady,
                        "RouteReady"),
                    null),
                EvaluationIntentType.MaterialFlow => new SchedulingPlanResult(
                    CreatePlanResultId(evaluationResult.ResultId),
                    evaluationResult.ResultId,
                    PlanningIntentType.Dispatch,
                    inputContext.Priority,
                    basis,
                    summary,
                    new DispatchPlanDecision(
                        new[]
                        {
                            new DispatchTask(
                                "DT-001",
                                evaluationResult.MaterialFlowDecision?.MaterialId ?? new MaterialId("MAT-UNKNOWN"),
                                evaluationResult.MaterialFlowDecision?.ModuleId,
                                null,
                                null,
                                "Transfer"),
                        },
                        PlanningConclusionKind.SkeletonReady,
                        "DispatchReady"),
                    null,
                    null),
                EvaluationIntentType.JobProgress => new SchedulingPlanResult(
                    CreatePlanResultId(evaluationResult.ResultId),
                    evaluationResult.ResultId,
                    PlanningIntentType.Dispatch,
                    inputContext.Priority,
                    basis,
                    summary,
                    new DispatchPlanDecision(
                        Array.Empty<DispatchTask>(),
                        PlanningConclusionKind.NeedCoordination,
                        "DispatchReserved"),
                    null,
                    null),
                EvaluationIntentType.Recovery => new SchedulingPlanResult(
                    CreatePlanResultId(evaluationResult.ResultId),
                    evaluationResult.ResultId,
                    PlanningIntentType.Recovery,
                    inputContext.Priority,
                    basis,
                    summary,
                    null,
                    null,
                    new RecoveryPlanDecision(
                        new RecoveryPlan(
                            evaluationResult.RecoveryDecision?.ProcessJobId ?? new ProcessJobId("PJ-UNKNOWN"),
                            evaluationResult.RecoveryDecision?.SuggestedReason ?? RecoveryReason.Unknown,
                            new[] { RecoveryActionType.Hold }),
                        PlanningConclusionKind.HoldForReview,
                        "RecoveryReady")),
                _ => new SchedulingPlanResult(
                    CreatePlanResultId(evaluationResult.ResultId),
                    evaluationResult.ResultId,
                    PlanningIntentType.Unknown,
                    inputContext.Priority,
                    basis,
                    new PlanningSummary(PlanningConclusionKind.Unknown, "Unknown", null),
                    null,
                    null,
                    null),
            };
        }

        private static string CreatePlanResultId(string evaluationResultId)
        {
            return $"plan-{evaluationResultId}";
        }
    }

    private sealed class StubPlanningResultCoordinator : IPlanningResultCoordinator
    {
        public IReadOnlyCollection<SchedulingPlanResult> Coordinate(
            IReadOnlyCollection<SchedulingPlanResult> results)
        {
            return results;
        }
    }

    private sealed class StubSchedulingPlanBuilder : ISchedulingPlanBuilder
    {
        private readonly IPlanningContextReader _contextReader;
        private readonly IEvaluationResultPlanner _planner;
        private readonly IPlanningResultCoordinator _coordinator;

        public StubSchedulingPlanBuilder(
            IPlanningContextReader contextReader,
            IEvaluationResultPlanner planner,
            IPlanningResultCoordinator coordinator)
        {
            _contextReader = contextReader;
            _planner = planner;
            _coordinator = coordinator;
        }

        public IReadOnlyCollection<SchedulingPlanResult> Build(
            IReadOnlyCollection<SchedulingEvaluationResult> evaluationResults,
            PlanningInputContext inputContext)
        {
            var results = evaluationResults
                .Select(evaluationResult =>
                {
                    var context = _contextReader.ReadFor(evaluationResult);
                    var mergedContext = context with
                    {
                        CorrelationId = inputContext.CorrelationId ?? context.CorrelationId,
                        PlannedAtUtc = inputContext.PlannedAtUtc,
                        ContextVersion = inputContext.ContextVersion ?? context.ContextVersion,
                        Priority = inputContext.Priority,
                    };

                    return _planner.Plan(evaluationResult, mergedContext);
                })
                .ToArray();

            return _coordinator.Coordinate(results);
        }
    }
}
