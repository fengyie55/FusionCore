using FusionUI.Models;
using FusionUI.Projections;

namespace FusionUI.ViewModels;

/// <summary>
/// 表示日志页的最小只读视图模型。
/// </summary>
public sealed class LogsViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 当前日志投影。
    /// </summary>
    public LogsViewProjection Projection { get; }

    /// <summary>
    /// 当前日志摘要集合。
    /// </summary>
    public IReadOnlyCollection<LogEntrySummaryModel> Entries => Projection.Entries;

    /// <summary>
    /// 初始化日志页视图模型。
    /// </summary>
    public LogsViewModel(LogsViewProjection? projection = null)
        : base("日志视图", "用于承载未来的运行日志与诊断信息入口。", "当前阶段仅提供最小日志摘要入口，不实现日志浏览系统。")
    {
        Projection = projection ?? LogsViewProjection.Empty;
    }
}
