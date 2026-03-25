namespace FusionUI.Models;

/// <summary>
/// 表示 UI 可消费的最小运行态摘要。
/// </summary>
public sealed record RuntimeSummaryModel(
    HostRuntimeSummaryModel Host,
    IReadOnlyCollection<ModuleRuntimeSummaryModel> Modules)
{
    /// <summary>
    /// 获取空运行态摘要。
    /// </summary>
    public static RuntimeSummaryModel Empty { get; } = new(
        new HostRuntimeSummaryModel(
            "未接线宿主",
            "Unknown",
            "NotInitialized",
            "n/a",
            null,
            "n/a"),
        Array.Empty<ModuleRuntimeSummaryModel>());
}
