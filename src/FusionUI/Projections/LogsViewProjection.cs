using FusionUI.Models;

namespace FusionUI.Projections;

/// <summary>
/// 表示日志页面可消费的最小只读投影。
/// </summary>
public sealed record LogsViewProjection(
    IReadOnlyCollection<LogEntrySummaryModel> Entries,
    string SummaryText)
{
    /// <summary>
    /// 获取空日志投影。
    /// </summary>
    public static LogsViewProjection Empty { get; } = new(
        Array.Empty<LogEntrySummaryModel>(),
        "当前尚未接入日志摘要来源。");
}
