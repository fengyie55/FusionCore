namespace FusionStudio.Layout;

/// <summary>
/// 表示 FusionStudio 壳层的最小布局语义。
/// </summary>
public sealed record StudioLayoutDescriptor(
    string TopAreaTitle,
    string NavigationAreaTitle,
    string WorkspaceAreaTitle,
    string FooterAreaTitle);
