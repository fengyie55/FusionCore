namespace FusionStudio.ViewModels;

/// <summary>
/// 表示模块状态浏览器的占位视图模型。
/// </summary>
public sealed class ModuleExplorerViewModel : PlaceholderViewModelBase
{
    public ModuleExplorerViewModel()
        : base(
            "模块状态",
            "用于平台模块摘要、连接状态与诊断入口的占位页面。",
            "当前阶段只保留只读状态浏览入口，不实现模块控制。")
    {
    }
}
