using FusionApp.Composition;
using FusionLog.Entries;
using FusionStudio.Composition;

namespace FusionStudio.Projections;

/// <summary>
/// 负责把应用装配结果投影为 FusionStudio 启动上下文。
/// </summary>
public static class StudioApplicationProjection
{
    /// <summary>
    /// 从应用装配结果创建工作台启动上下文。
    /// </summary>
    public static StudioBootstrapContext CreateBootstrapContext(
        ApplicationAssembly assembly,
        IReadOnlyCollection<LogEntry>? entries = null)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var runtimeSummary = StudioRuntimeProjection.FromDiagnostic(assembly.Runtime.Host.DiagnosticInfo);
        var configurationSummary = StudioConfigurationProjection.FromAssembly(assembly);
        var logSummary = StudioLogProjection.FromEntries(entries);

        return new StudioBootstrapContext(
            new StudioShellOptions(
                "FusionStudio",
                "平台工程工作台",
                assembly.UiBootstrapDescriptor.StartupMessage),
            new StudioNavigationOptions(
                true,
                true,
                true,
                true,
                true),
            new StudioRuntimeDescriptor(
                assembly.UiBootstrapDescriptor.DisplayTitle,
                runtimeSummary.Profile,
                runtimeSummary.RuntimeRoot,
                CreateDependencies(assembly)),
            configurationSummary,
            runtimeSummary,
            logSummary);
    }

    private static IReadOnlyCollection<StudioDependencyDescriptor> CreateDependencies(ApplicationAssembly assembly)
    {
        return
        [
            new StudioDependencyDescriptor(
                "FusionKernel",
                true,
                "应用装配结果已携带宿主运行摘要。"),
            new StudioDependencyDescriptor(
                "FusionConfig",
                assembly.Boundary.ConfigurationProvider is not null || assembly.Boundary.ConfigurationSnapshot is not null,
                assembly.Boundary.ConfigurationProvider is null && assembly.Boundary.ConfigurationSnapshot is null
                    ? "当前尚未注入配置边界。"
                    : "应用装配结果已携带最小配置边界。"),
            new StudioDependencyDescriptor(
                "FusionLog",
                assembly.Boundary.LoggerWriter is not null || assembly.Boundary.LoggerContext is not null,
                assembly.Boundary.LoggerWriter is null && assembly.Boundary.LoggerContext is null
                    ? "当前尚未注入日志边界。"
                    : "应用装配结果已携带最小日志边界。"),
            new StudioDependencyDescriptor(
                "FusionApp",
                true,
                "当前工作台由应用装配结果驱动默认接线。")
        ];
    }
}
