using FusionUI.Models;
using FusionUI.Projections;

namespace FusionUI.Composition;

/// <summary>
/// 表示 UI 壳层启动时的最小接线上下文。
/// </summary>
public sealed record UiBootstrapContext(
    UiSectionMappingResult? MappingResult,
    RuntimeSummaryModel? RuntimeSummary,
    LogsViewProjection? LogsProjection,
    IReadOnlyCollection<UiDependencyDescriptor>? Dependencies);
