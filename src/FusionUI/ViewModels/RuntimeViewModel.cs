using FusionUI.Models;

namespace FusionUI.ViewModels;

/// <summary>
/// 表示运行页的最小只读视图模型。
/// </summary>
public sealed class RuntimeViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 当前运行态摘要。
    /// </summary>
    public RuntimeSummaryModel Summary { get; }

    /// <summary>
    /// 当前宿主摘要。
    /// </summary>
    public HostRuntimeSummaryModel Host => Summary.Host;

    /// <summary>
    /// 当前模块摘要集合。
    /// </summary>
    public IReadOnlyCollection<ModuleRuntimeSummaryModel> Modules => Summary.Modules;

    /// <summary>
    /// 初始化运行页视图模型。
    /// </summary>
    public RuntimeViewModel(RuntimeSummaryModel? summary = null)
        : base("运行视图", "用于承载未来的宿主状态、模块状态与运行摘要。", "当前阶段仅提供最小只读运行摘要入口。")
    {
        Summary = summary ?? RuntimeSummaryModel.Empty;
    }
}
