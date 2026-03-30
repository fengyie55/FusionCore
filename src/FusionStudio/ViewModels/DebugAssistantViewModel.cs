namespace FusionStudio.ViewModels;

/// <summary>
/// 表示调试助手工作页的占位视图模型。
/// </summary>
public sealed class DebugAssistantViewModel : PlaceholderViewModelBase
{
    public DebugAssistantViewModel()
        : base(
            "调试助手",
            "用于承载现场联调辅助、问题定位提示与工程工具扩展入口。",
            "当前阶段只保留辅助入口，不实现命令下发与自动调试逻辑。")
    {
    }
}
