using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示模块工作台页面的视图模型。
/// </summary>
public sealed class ModuleWorkbenchViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取模块集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleNodeModel> Modules { get; }

    public ModuleWorkbenchViewModel(IReadOnlyCollection<StudioModuleNodeModel> modules)
        : base(
            "模块工作台",
            "按模块聚合参数、状态、报警、互锁、IO 与调试入口。",
            "当前阶段只提供模块树与工具域骨架，不实现真实控制面板。")
    {
        Modules = modules;
    }
}
