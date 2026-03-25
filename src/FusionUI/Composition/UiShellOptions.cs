namespace FusionUI.Composition;

/// <summary>
/// 表示 UI 壳层的最小选项。
/// </summary>
public sealed record UiShellOptions(
    string ApplicationTitle,
    string ShellSubtitle,
    string StartupMessage);
