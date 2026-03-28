namespace FusionStudio.Models;

/// <summary>
/// 表示 FusionStudio 状态栏的最小摘要模型。
/// </summary>
public sealed record StudioStatusModel(
    IReadOnlyCollection<StudioStatusItem> Items,
    string Message)
{
    /// <summary>
    /// 获取空状态模型。
    /// </summary>
    public static StudioStatusModel Empty { get; } = new(Array.Empty<StudioStatusItem>(), string.Empty);
}
