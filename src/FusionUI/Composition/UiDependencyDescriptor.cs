namespace FusionUI.Composition;

/// <summary>
/// 表示 UI 当前依赖边界的最小描述信息。
/// </summary>
public sealed record UiDependencyDescriptor(
    string DependencyName,
    bool IsConnected,
    string? Description);
