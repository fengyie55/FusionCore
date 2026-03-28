namespace FusionStudio.ViewModels;

/// <summary>
/// 表示运行诊断工作台的占位视图模型。
/// </summary>
public sealed class RuntimeDiagnosticsViewModel : PlaceholderViewModelBase
{
    public RuntimeDiagnosticsViewModel()
        : base(
            "运行诊断",
            "用于宿主状态、模块状态和运行摘要入口的占位页面。",
            "当前阶段只保留只读摘要入口，不实现诊断系统。")
    {
    }
}
