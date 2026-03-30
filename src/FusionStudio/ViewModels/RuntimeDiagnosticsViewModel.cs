using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示运行诊断工作页的占位视图模型。
/// </summary>
public sealed class RuntimeDiagnosticsViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取设备总览摘要。
    /// </summary>
    public StudioDeviceOverviewModel Overview { get; }

    /// <summary>
    /// 获取运行态摘要。
    /// </summary>
    public StudioRuntimeSummaryModel Summary { get; }

    public RuntimeDiagnosticsViewModel(
        StudioDeviceOverviewModel overview,
        StudioRuntimeSummaryModel summary)
        : base(
            "运行诊断",
            "用于查看宿主、运行实例与模块状态摘要的只读工程入口。",
            "当前阶段只承载诊断摘要，不实现健康检查平台与自动恢复机制。")
    {
        Overview = overview;
        Summary = summary;
    }
}
