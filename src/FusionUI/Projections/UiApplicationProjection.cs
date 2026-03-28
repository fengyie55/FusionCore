using FusionApp.Composition;
using FusionConfig.Sections;
using FusionUI.Composition;
using FusionUI.Models;

namespace FusionUI.Projections;

/// <summary>
/// 负责把应用装配结果投影为 UI 可消费的最小启动上下文。
/// </summary>
public static class UiApplicationProjection
{
    /// <summary>
    /// 从应用装配结果创建 UI 启动上下文。
    /// </summary>
    public static UiBootstrapContext CreateBootstrapContext(ApplicationAssembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var mappingResult = CreateMappingResult(assembly);
        var runtimeSummary = UiRuntimeProjection.FromDiagnostic(assembly.Runtime.Host.DiagnosticInfo);

        return new UiBootstrapContext(
            mappingResult,
            runtimeSummary,
            LogsViewProjection.Empty,
            CreateDependencies(assembly));
    }

    private static UiSectionMappingResult CreateMappingResult(ApplicationAssembly assembly)
    {
        var provider = assembly.Boundary.ConfigurationProvider;
        if (provider is not null &&
            provider.TryGetSection<UiSection>(ConfigurationSectionKeys.Ui, out var uiSection) &&
            uiSection is not null)
        {
            return UiOptionsBinder.Bind(uiSection);
        }

        return UiOptionsBinder.Bind(null);
    }

    private static IReadOnlyCollection<UiDependencyDescriptor> CreateDependencies(ApplicationAssembly assembly)
    {
        return
        [
            new UiDependencyDescriptor(
                "Config",
                assembly.Boundary.ConfigurationProvider is not null || assembly.Boundary.ConfigurationSnapshot is not null,
                assembly.Boundary.ConfigurationProvider is null && assembly.Boundary.ConfigurationSnapshot is null
                    ? "当前尚未注入配置边界。"
                    : "应用装配结果已携带最小配置边界。"),
            new UiDependencyDescriptor(
                "Runtime",
                true,
                "应用装配结果已携带宿主运行摘要。"),
            new UiDependencyDescriptor(
                "Logs",
                assembly.Boundary.LoggerWriter is not null || assembly.Boundary.LoggerContext is not null,
                assembly.Boundary.LoggerWriter is null && assembly.Boundary.LoggerContext is null
                    ? "当前尚未注入日志边界。"
                    : "应用装配结果已携带最小日志边界。")
        ];
    }
}
