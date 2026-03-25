namespace FusionUI.Models;

/// <summary>
/// 表示 UI 状态栏的最小只读模型。
/// </summary>
public sealed record StatusBarModel(
    IReadOnlyCollection<StatusBarItem> Items,
    UiStatusMessage Message)
{
    /// <summary>
    /// 获取空状态栏模型。
    /// </summary>
    public static StatusBarModel Empty { get; } = new(
        Array.Empty<StatusBarItem>(),
        new UiStatusMessage("界面尚未完成最小接线。"));
}
