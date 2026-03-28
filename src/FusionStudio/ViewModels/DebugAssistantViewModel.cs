namespace FusionStudio.ViewModels;

/// <summary>
/// 表示调试助手的占位视图模型。
/// </summary>
public sealed class DebugAssistantViewModel : PlaceholderViewModelBase
{
    public DebugAssistantViewModel()
        : base(
            "调试助手",
            "用于开发与现场调试辅助工具入口的占位页面。",
            "当前阶段只保留调试入口编排，不实现调试工具集。")
    {
    }
}
