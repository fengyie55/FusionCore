using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示运行诊断工作台的只读视图模型。
/// </summary>
public sealed class RuntimeDiagnosticsViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取宿主运行摘要。
    /// </summary>
    public StudioRuntimeSummaryModel Summary { get; }

    public RuntimeDiagnosticsViewModel(StudioRuntimeSummaryModel? summary = null)
        : base(
            "运行诊断",
            "用于宿主状态、模块状态和运行摘要入口的只读页面。",
            "当前阶段只保留只读摘要入口，不实现诊断系统。")
    {
        Summary = summary ?? StudioRuntimeSummaryModel.Empty;
    }
}
