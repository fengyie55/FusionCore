namespace FusionStudio.Composition;

/// <summary>
/// 表示 FusionStudio 壳层构造时的最小接线上下文。
/// </summary>
public sealed record StudioBootstrapContext(
    StudioShellOptions ShellOptions,
    StudioNavigationOptions NavigationOptions,
    StudioRuntimeDescriptor RuntimeDescriptor);
