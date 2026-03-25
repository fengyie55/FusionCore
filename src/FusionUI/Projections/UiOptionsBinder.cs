using FusionConfig.Sections;
using FusionUI.Composition;

namespace FusionUI.Projections;

/// <summary>
/// 负责将 UI 配置节映射为界面所需的最小选项。
/// </summary>
public static class UiOptionsBinder
{
    /// <summary>
    /// 将配置节映射为 UI 选项。
    /// </summary>
    public static UiSectionMappingResult Bind(UiSection? section)
    {
        if (section is null)
        {
            return new UiSectionMappingResult(
                false,
                CreateDefaultShellOptions(),
                CreateDefaultNavigationOptions(),
                CreateDefaultStatusBarOptions(),
                "未提供 UI 配置节，已回退到默认界面选项。");
        }

        if (!section.Enabled)
        {
            return new UiSectionMappingResult(
                false,
                CreateDefaultShellOptions() with { ShellSubtitle = "UI 模块当前被配置为禁用状态。" },
                CreateDefaultNavigationOptions(),
                CreateDefaultStatusBarOptions(),
                "UI 配置节指示当前界面模块未启用。");
        }

        return new UiSectionMappingResult(
            true,
            CreateDefaultShellOptions() with
            {
                StartupMessage = $"已载入 UI 配置目录：{section.ConfigPath}"
            },
            CreateDefaultNavigationOptions(),
            CreateDefaultStatusBarOptions(),
            null);
    }

    internal static UiShellOptions CreateDefaultShellOptions()
    {
        return new UiShellOptions(
            "FusionCore",
            "E95 对齐的最小操作界面壳层",
            "当前阶段仅提供最小配置接线与只读摘要入口。");
    }

    internal static UiNavigationOptions CreateDefaultNavigationOptions()
    {
        return new UiNavigationOptions();
    }

    internal static UiStatusBarOptions CreateDefaultStatusBarOptions()
    {
        return new UiStatusBarOptions();
    }
}
