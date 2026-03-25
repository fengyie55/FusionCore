namespace FusionUI.Models;

/// <summary>
/// 表示日志入口的最小只读摘要。
/// </summary>
public sealed record LogEntrySummaryModel(
    DateTimeOffset Timestamp,
    string Level,
    string Category,
    string Message,
    string Source);
