using FusionApp.Runtime;

namespace FusionApp.Composition;

/// <summary>
/// 表示面向 FusionStudio 的最小启动摘要。
/// </summary>
/// <param name="ShellTitle">壳层标题。</param>
/// <param name="StartRoute">起始路由。</param>
/// <param name="StartupMessage">启动消息。</param>
/// <param name="ReadOnlyEntryPoints">只读入口列表。</param>
/// <param name="RuntimeDescriptor">运行摘要。</param>
public sealed record ApplicationStudioBootstrapDescriptor(
    string ShellTitle,
    string StartRoute,
    string StartupMessage,
    IReadOnlyList<string> ReadOnlyEntryPoints,
    ApplicationRuntimeDescriptor RuntimeDescriptor)
{
    /// <summary>
    /// 获取显示标题。
    /// </summary>
    public string DisplayTitle => string.IsNullOrWhiteSpace(ShellTitle)
        ? RuntimeDescriptor.DisplayTitle
        : ShellTitle;
}
