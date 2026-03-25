namespace FusionUI.ViewModels;

/// <summary>
/// 表示设备页面占位视图模型。
/// </summary>
public sealed class EquipmentViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 初始化设备页面占位视图模型。
    /// </summary>
    public EquipmentViewModel()
        : base("设备视图", "用于承载未来的模块视图与设备结构可视化入口。", "当前阶段不实现设备控制或模块动作。")
    {
    }
}
