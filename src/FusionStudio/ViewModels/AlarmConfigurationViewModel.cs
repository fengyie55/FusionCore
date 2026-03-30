using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示报警配置工作页的占位视图模型。
/// </summary>
public sealed class AlarmConfigurationViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取模块集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleNodeModel> Modules { get; }

    public AlarmConfigurationViewModel(IReadOnlyCollection<StudioModuleNodeModel> modules)
        : base(
            "报警配置",
            "按模块组织报警项、报警级别与工程映射关系的入口占位。",
            "当前阶段仅展示模块范围与配置语义，不实现报警编辑器。")
    {
        Modules = modules;
    }
}
