namespace FusionStudio.ViewModels;

/// <summary>
/// 表示工程配置工作台的占位视图模型。
/// </summary>
public sealed class ConfigurationWorkbenchViewModel : PlaceholderViewModelBase
{
    public ConfigurationWorkbenchViewModel()
        : base(
            "工程配置",
            "用于设备工程配置、模板配置与运行实例配置入口的占位页面。",
            "当前阶段只保留结构与信息架构，不实现配置编辑器。")
    {
    }
}
