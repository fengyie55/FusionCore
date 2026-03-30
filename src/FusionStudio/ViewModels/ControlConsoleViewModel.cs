using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示工程控制台页的只读视图模型。
/// </summary>
public sealed class ControlConsoleViewModel : PlaceholderViewModelBase
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
    /// 初始化工程控制台视图模型。
    /// </summary>
    public ControlConsoleViewModel(
        IReadOnlyCollection<StudioModuleNodeModel> modules,
        StudioToolPageContextModel context)
        : base(
            "工程控制台",
            "用于承载模块工程指令、手动测试与控制入口。",
            "当前阶段只保留入口骨架，不直接执行后台控制命令。")
    {
        Modules = modules;
        Context = context;
    }
}
