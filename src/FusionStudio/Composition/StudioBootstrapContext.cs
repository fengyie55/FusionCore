using FusionStudio.Models;

namespace FusionStudio.Composition;

/// <summary>
/// 表示 FusionStudio 壳层构造时的最小接线上下文。
/// </summary>
public sealed record StudioBootstrapContext(
    StudioShellOptions ShellOptions,
    StudioNavigationOptions NavigationOptions,
    StudioRuntimeDescriptor RuntimeDescriptor,
    StudioConfigurationSummaryModel ConfigurationSummary,
    StudioRuntimeSummaryModel RuntimeSummary,
    StudioLogSummaryModel LogSummary,
    StudioDeviceOverviewModel DeviceOverview,
    StudioEngineeringTreeModel EngineeringTree,
    IReadOnlyCollection<StudioModuleContextModel> ModuleContexts);
