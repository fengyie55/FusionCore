namespace FusionStudio.Models;

/// <summary>
/// 表示详细日志入口的最小只读摘要。
/// </summary>
public sealed record StudioLogSummaryModel(
    IReadOnlyCollection<StudioLogEntrySummaryModel> Entries,
    string SummaryText)
{
    /// <summary>
    /// 获取空日志摘要。
    /// </summary>
    public static StudioLogSummaryModel Empty { get; } =
        new(Array.Empty<StudioLogEntrySummaryModel>(), "当前尚未接入日志摘要。");
}
