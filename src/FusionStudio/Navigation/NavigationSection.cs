namespace FusionStudio.Navigation;

/// <summary>
/// 表示一组导航项的分区。
/// </summary>
public sealed record NavigationSection(
    string Title,
    IReadOnlyList<NavigationItem> Items);
