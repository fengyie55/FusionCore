namespace FusionUI.Layout;

/// <summary>
/// 描述 Shell 的最小布局分区语义。
/// </summary>
public sealed record ShellLayoutDescriptor(
    string TopAreaTitle,
    string NavigationAreaTitle,
    string WorkspaceAreaTitle,
    string FooterAreaTitle);
