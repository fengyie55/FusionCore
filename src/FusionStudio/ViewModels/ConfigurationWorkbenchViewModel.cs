using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示工程配置工作台的占位视图模型。
/// </summary>
public sealed class ConfigurationWorkbenchViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取设备总览摘要。
    /// </summary>
    public StudioDeviceOverviewModel Overview { get; }

    public ConfigurationWorkbenchViewModel(StudioDeviceOverviewModel overview)
        : base(
            "工程配置",
            "用于组织整机、模块与运行实例配置入口的工程工作页。",
            "当前阶段只展示整机与模块配置范围，不实现真实配置编辑器。")
    {
        Overview = overview;
    }
}
