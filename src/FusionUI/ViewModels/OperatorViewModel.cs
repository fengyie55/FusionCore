namespace FusionUI.ViewModels;

/// <summary>
/// 表示操作员页面占位视图模型。
/// </summary>
public sealed class OperatorViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 初始化操作员页面占位视图模型。
    /// </summary>
    public OperatorViewModel()
        : base("操作员视图", "用于承载未来的操作员工作区和受控运行入口。", "当前阶段不实现运行控制逻辑。")
    {
    }
}
