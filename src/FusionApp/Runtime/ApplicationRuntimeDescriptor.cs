using FusionConfig.Runtime;
using FusionKernel.Hosting;

namespace FusionApp.Runtime;

/// <summary>
/// 表示 FusionApp 的最小只读运行摘要。
/// </summary>
public sealed record ApplicationRuntimeDescriptor(
    string ApplicationId,
    string ApplicationTitle,
    string HostId,
    string HostName,
    string RuntimeInstanceId,
    RuntimeRootOptions RuntimeRoot,
    HostRunMode RunMode,
    string? Profile,
    string StartupMessage,
    string StartRoute,
    IReadOnlyList<string> ReadOnlyEntryPoints,
    IReadOnlyList<string> ModuleNames)
{
    /// <summary>
    /// 获取逻辑应用标题。
    /// </summary>
    public string DisplayTitle => string.IsNullOrWhiteSpace(ApplicationTitle) ? ApplicationId : ApplicationTitle;

    /// <summary>
    /// 获取运行根的物理路径摘要。
    /// </summary>
    public string RuntimeRootPath => RuntimeRoot.PhysicalRoot;
}
