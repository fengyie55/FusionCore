namespace FusionUI.ViewModels;

/// <summary>
/// 表示工程师页面占位视图模型。
/// </summary>
public sealed class EngineerViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 初始化工程师页面占位视图模型。
    /// </summary>
    public EngineerViewModel()
        : base("工程师视图", "用于承载未来的工程配置、诊断入口与调试页面。", "当前阶段不实现配方、诊断或调参逻辑。")
    {
    }
}
