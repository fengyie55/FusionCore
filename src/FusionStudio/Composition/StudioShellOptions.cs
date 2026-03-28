namespace FusionStudio.Composition;

/// <summary>
/// 表示 FusionStudio 壳层所需的最小显示选项。
/// </summary>
public sealed record StudioShellOptions(
    string ApplicationTitle,
    string ShellSubtitle,
    string StartupMessage);
