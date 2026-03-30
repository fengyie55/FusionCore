namespace FusionStudio.Models;

/// <summary>
/// 表示宿主运行态的最小只读摘要。
/// </summary>
public sealed record StudioRuntimeSummaryModel(
    string HostName,
    string HostState,
    string InitializationState,
    string RuntimeInstanceId,
    string Profile,
    string RuntimeRoot,
    IReadOnlyCollection<StudioModuleSummaryModel> Modules)
{
    /// <summary>
    /// 获取空运行态摘要。
    /// </summary>
    public static StudioRuntimeSummaryModel Empty { get; } =
        new(
            "未接入",
            "Unknown",
            "Unknown",
            "n/a",
            "n/a",
            "n/a",
            Array.Empty<StudioModuleSummaryModel>());
}
