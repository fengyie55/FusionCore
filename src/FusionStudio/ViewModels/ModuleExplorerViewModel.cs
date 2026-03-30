using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示模块状态浏览器的只读视图模型。
/// </summary>
public sealed class ModuleExplorerViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取模块摘要集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleSummaryModel> Modules { get; }

    public ModuleExplorerViewModel(IReadOnlyCollection<StudioModuleSummaryModel>? modules = null)
        : base(
            "模块状态",
            "用于平台模块摘要、连接状态与诊断入口的只读页面。",
            "当前阶段只保留只读状态浏览入口，不实现模块控制。")
    {
        Modules = modules ?? Array.Empty<StudioModuleSummaryModel>();
    }
}
