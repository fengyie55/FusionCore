namespace FusionUI.ViewModels;

/// <summary>
/// 表示概览页占位视图模型。
/// </summary>
public sealed class OverviewViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 初始化概览页占位视图模型。
    /// </summary>
    public OverviewViewModel()
        : base("概览", "用于承载未来的设备概览、运行摘要与 E95 总览视图。", "当前阶段仅提供导航落点与布局占位。")
    {
    }
}
