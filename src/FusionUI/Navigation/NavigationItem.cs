namespace FusionUI.Navigation;

/// <summary>
/// 表示一个最小导航项。
/// </summary>
public sealed record NavigationItem(
    UiRoute Route,
    string Title,
    string Description,
    string SectionTitle);
