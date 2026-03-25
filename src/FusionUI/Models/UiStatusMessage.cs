namespace FusionUI.Models;

/// <summary>
/// 表示状态栏中的最小消息文本。
/// </summary>
public sealed record UiStatusMessage(
    string Text,
    bool IsHighlighted = false);
