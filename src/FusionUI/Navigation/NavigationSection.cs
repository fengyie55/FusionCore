namespace FusionUI.Navigation;

/// <summary>
/// 表示一组导航项的逻辑分区。
/// </summary>
public sealed record NavigationSection(
    string Title,
    IReadOnlyList<NavigationItem> Items);
