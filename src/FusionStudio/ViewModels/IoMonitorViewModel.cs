using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示 IO 监控页面的占位视图模型。
/// </summary>
public sealed class IoMonitorViewModel : PlaceholderViewModelBase
{
    /// <summary>
    /// 获取模块集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleNodeModel> Modules { get; }

    public IoMonitorViewModel(IReadOnlyCollection<StudioModuleNodeModel> modules)
        : base(
            "IO 监控",
            "用于查看模块 IO 信号摘要、映射入口与联调观察位。",
            "当前阶段只展示模块级 IO 工具入口，不实现实时监控引擎。")
    {
        Modules = modules;
    }
}
