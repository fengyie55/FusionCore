using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示互锁管理工作页的只读视图模型。
/// </summary>
public sealed class InterlockManagementViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取模块集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleNodeModel> Modules { get; }

    /// <summary>
    /// 获取统一工具页上下文。
    /// </summary>
    public StudioToolPageContextModel Context { get; }

    /// <summary>
    /// 初始化互锁管理视图模型。
    /// </summary>
    public InterlockManagementViewModel(
        IReadOnlyCollection<StudioModuleNodeModel> modules,
        StudioToolPageContextModel context)
        : base(
            "互锁管理",
            "用于查看模块互锁、跨模块互锁与调试约束入口的占位页。",
            "当前阶段仅保留互锁信息骨架，不实现互锁编辑与求值逻辑。")
    {
        Modules = modules;
        Context = context;
    }
}
