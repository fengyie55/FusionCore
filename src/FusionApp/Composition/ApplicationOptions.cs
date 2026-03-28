using FusionConfig.Runtime;
using FusionKernel.Hosting;

namespace FusionApp.Composition;

/// <summary>
/// 表示 FusionApp 的最小应用装配选项。
/// </summary>
public sealed record ApplicationOptions(
    string ApplicationId,
    string ApplicationTitle,
    string HostId,
    string HostName,
    string RuntimeInstanceId,
    RuntimeRootOptions RuntimeRoot,
    HostRunMode RunMode,
    string? Profile,
    string StartupMessage,
    IReadOnlyList<string>? ReadOnlyEntryPoints = null)
{
    /// <summary>
    /// 获取应用显示名称。
    /// </summary>
    public string DisplayName => string.IsNullOrWhiteSpace(ApplicationTitle) ? ApplicationId : ApplicationTitle;
}
