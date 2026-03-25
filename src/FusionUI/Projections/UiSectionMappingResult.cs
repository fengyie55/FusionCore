using FusionUI.Composition;

namespace FusionUI.Projections;

/// <summary>
/// 表示 UI 配置节映射后的最小结果。
/// </summary>
public sealed record UiSectionMappingResult(
    bool IsSuccess,
    UiShellOptions ShellOptions,
    UiNavigationOptions NavigationOptions,
    UiStatusBarOptions StatusBarOptions,
    string? Message);
