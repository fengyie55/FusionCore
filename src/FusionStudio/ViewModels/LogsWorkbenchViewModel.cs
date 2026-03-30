using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示详细日志工作台的只读视图模型。
/// </summary>
public sealed class LogsWorkbenchViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取日志摘要。
    /// </summary>
    public StudioLogSummaryModel Summary { get; }

    public LogsWorkbenchViewModel(StudioLogSummaryModel? summary = null)
        : base(
            "详细日志",
            "用于日志检视、故障追踪与现场排查入口的只读页面。",
            "当前阶段只保留入口，不实现日志浏览、过滤与搜索。")
    {
        Summary = summary ?? StudioLogSummaryModel.Empty;
    }
}
