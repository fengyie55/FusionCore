namespace FusionUI.ViewModels;

/// <summary>
/// 表示日志页面占位视图模型。
/// </summary>
public sealed class LogsViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 初始化日志页面占位视图模型。
    /// </summary>
    public LogsViewModel()
        : base("日志视图", "用于承载未来的运行日志与诊断信息入口。", "当前阶段不实现日志浏览、检索或远程采集。")
    {
    }
}
