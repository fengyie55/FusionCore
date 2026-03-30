using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示详细日志工作页的占位视图模型。
/// </summary>
public sealed class LogsWorkbenchViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取模块集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleNodeModel> Modules { get; }

    public LogsWorkbenchViewModel(IReadOnlyCollection<StudioModuleNodeModel> modules)
        : base(
            "详细日志",
            "用于承载整机与模块详细日志入口、故障追踪入口与联调摘要。",
            "当前阶段只保留日志入口骨架，不实现检索、过滤与实时流。")
    {
        Modules = modules;
    }
}
