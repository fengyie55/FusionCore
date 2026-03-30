using FusionApp.Composition;
using FusionConfig.Sections;
using FusionStudio.Models;

namespace FusionStudio.Projections;

/// <summary>
/// 负责把应用装配结果投影为工程配置入口摘要。
/// </summary>
public static class StudioConfigurationProjection
{
    /// <summary>
    /// 从应用装配结果创建工程配置摘要。
    /// </summary>
    public static StudioConfigurationSummaryModel FromAssembly(ApplicationAssembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var provider = assembly.Boundary.ConfigurationProvider;
        if (provider is not null &&
            provider.TryGetSection<UiSection>(ConfigurationSectionKeys.Ui, out var uiSection) &&
            uiSection is not null)
        {
            return new StudioConfigurationSummaryModel(
                true,
                uiSection.ConfigPath,
                "当前通过配置提供者接入最小工程配置目录。");
        }

        return new StudioConfigurationSummaryModel(
            assembly.Boundary.ConfigurationProvider is not null || assembly.Boundary.ConfigurationSnapshot is not null,
            assembly.RuntimeDescriptor.RuntimeRoot.ConfigPath,
            "当前通过运行根约定回退到配置目录摘要。");
    }
}
