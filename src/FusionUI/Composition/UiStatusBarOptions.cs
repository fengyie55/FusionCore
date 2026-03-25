namespace FusionUI.Composition;

/// <summary>
/// 表示状态栏显示规则的最小选项。
/// </summary>
public sealed record UiStatusBarOptions(
    bool ShowCurrentPage = true,
    bool ShowHostState = true,
    bool ShowProfile = true,
    bool ShowRuntimeRoot = true);
