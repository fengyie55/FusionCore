namespace FusionStudio.Navigation;

/// <summary>
/// 表示一个最小导航入口。
/// </summary>
public sealed record NavigationItem(
    StudioRoute Route,
    string Title,
    string Description,
    string SectionTitle);
