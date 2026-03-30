using FusionLog.Entries;
using FusionStudio.Models;

namespace FusionStudio.Projections;

/// <summary>
/// 负责把日志条目集合投影为工作台可读日志摘要。
/// </summary>
public static class StudioLogProjection
{
    /// <summary>
    /// 从日志条目创建最小日志摘要。
    /// </summary>
    public static StudioLogSummaryModel FromEntries(IReadOnlyCollection<LogEntry>? entries)
    {
        if (entries is null || entries.Count == 0)
        {
            return StudioLogSummaryModel.Empty;
        }

        var summaries = entries
            .OrderByDescending(entry => entry.Timestamp)
            .Take(20)
            .Select(entry => new StudioLogEntrySummaryModel(
                entry.Timestamp,
                entry.Level.ToString(),
                entry.Category.Name,
                entry.Message.Text,
                BuildSource(entry)))
            .ToArray();

        return new StudioLogSummaryModel(
            summaries,
            $"当前显示 {summaries.Length} 条日志摘要。");
    }

    private static string BuildSource(LogEntry entry)
    {
        var module = entry.Context.Module?.ModuleName;
        var process = entry.Context.Process?.ProcessName;
        var host = entry.Context.Host?.HostName;

        return module
            ?? process
            ?? host
            ?? "未知来源";
    }
}
