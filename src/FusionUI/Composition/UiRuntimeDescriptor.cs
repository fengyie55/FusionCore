using FusionUI.Layout;
using FusionUI.Models;

namespace FusionUI.Composition;

/// <summary>
/// 描述当前 UI 壳层的最小运行态信息。
/// </summary>
public sealed record UiRuntimeDescriptor(
    string ApplicationTitle,
    ShellLayoutDescriptor Layout,
    IReadOnlyList<string> NavigationSections,
    RuntimeSummaryModel RuntimeSummary,
    IReadOnlyCollection<UiDependencyDescriptor> Dependencies);
