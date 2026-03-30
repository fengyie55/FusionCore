using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示互锁管理工作页的占位视图模型。
/// </summary>
public sealed class InterlockManagementViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取模块集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleNodeModel> Modules { get; }

    public InterlockManagementViewModel(IReadOnlyCollection<StudioModuleNodeModel> modules)
        : base(
            "互锁管理",
            "用于查看模块互锁、跨模块互锁与调试约束入口的占位页。",
            "当前阶段只保留互锁信息架构，不实现互锁编辑与求值逻辑。")
    {
        Modules = modules;
    }
}
