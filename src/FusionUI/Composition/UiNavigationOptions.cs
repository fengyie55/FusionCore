namespace FusionUI.Composition;

/// <summary>
/// 表示 UI 导航骨架的最小选项。
/// </summary>
public sealed record UiNavigationOptions(
    bool IncludeRuntimeEntry = true,
    bool IncludeLogsEntry = true,
    bool IncludeEquipmentEntry = true);
