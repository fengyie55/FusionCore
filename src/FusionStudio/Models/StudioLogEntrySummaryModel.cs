namespace FusionStudio.Models;

/// <summary>
/// 表示日志入口的最小只读条目摘要。
/// </summary>
public sealed record StudioLogEntrySummaryModel(
    DateTimeOffset Timestamp,
    string Level,
    string Category,
    string Message,
    string Source);
