namespace FusionUI.ViewModels;

/// <summary>
/// 表示运行时页面占位视图模型。
/// </summary>
public sealed class RuntimeViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 初始化运行时页面占位视图模型。
    /// </summary>
    public RuntimeViewModel()
        : base("运行视图", "用于承载未来的宿主状态、进程状态与运行摘要。", "当前阶段仅提供运行入口占位，不实现健康检查系统。")
    {
    }
}
