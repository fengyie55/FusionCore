namespace FusionStudio.Models;

/// <summary>
/// 表示状态栏中的一个最小条目。
/// </summary>
public sealed record StudioStatusItem(
    string Label,
    string Value);
