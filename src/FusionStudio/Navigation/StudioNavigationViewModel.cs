namespace FusionStudio.Navigation;

/// <summary>
/// 表示 FusionStudio 的最小导航视图模型。
/// </summary>
public sealed class StudioNavigationViewModel
{
    /// <summary>
    /// 当前导航分区。
    /// </summary>
    public IReadOnlyList<NavigationSection> Sections { get; }

    /// <summary>
    /// 当前选中项。
    /// </summary>
    public NavigationItem? SelectedItem { get; private set; }

    /// <summary>
    /// 初始化导航视图模型。
    /// </summary>
    public StudioNavigationViewModel(IReadOnlyList<NavigationSection> sections)
    {
        Sections = sections;
    }

    /// <summary>
    /// 选择一个导航项。
    /// </summary>
    public void Select(NavigationItem item)
    {
        SelectedItem = item;
    }
}
