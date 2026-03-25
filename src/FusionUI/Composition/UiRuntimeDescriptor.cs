using FusionUI.Layout;

namespace FusionUI.Composition;

/// <summary>
/// 描述当前 UI 壳的最小运行时信息。
/// </summary>
public sealed record UiRuntimeDescriptor(
    string ApplicationTitle,
    ShellLayoutDescriptor Layout,
    IReadOnlyList<string> NavigationSections);
