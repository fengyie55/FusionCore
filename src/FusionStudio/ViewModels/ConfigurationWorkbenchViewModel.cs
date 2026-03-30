using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示工程配置工作台的只读视图模型。
/// </summary>
public sealed class ConfigurationWorkbenchViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取配置入口摘要。
    /// </summary>
    public StudioConfigurationSummaryModel Summary { get; }

    public ConfigurationWorkbenchViewModel(StudioConfigurationSummaryModel? summary = null)
        : base(
            "工程配置",
            "用于设备工程配置、模板配置与运行实例配置入口的只读页面。",
            "当前阶段只保留结构与信息架构，不实现配置编辑器。")
    {
        Summary = summary ?? StudioConfigurationSummaryModel.Empty;
    }
}
