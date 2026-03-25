using FusionUI.ViewModels;

namespace FusionUI.Navigation;

/// <summary>
/// 表示最小导航视图模型。
/// </summary>
public sealed class NavigationViewModel : ObservableObject
{
    private NavigationItem? _selectedItem;

    /// <summary>
    /// 导航分区集合。
    /// </summary>
    public IReadOnlyList<NavigationSection> Sections { get; }

    /// <summary>
    /// 当前选中的导航项。
    /// </summary>
    public NavigationItem? SelectedItem
    {
        get => _selectedItem;
        private set => SetProperty(ref _selectedItem, value);
    }

    /// <summary>
    /// 初始化最小导航视图模型。
    /// </summary>
    public NavigationViewModel(IReadOnlyList<NavigationSection> sections)
    {
        Sections = sections;
    }

    /// <summary>
    /// 设置当前导航项。
    /// </summary>
    public void Select(NavigationItem item)
    {
        SelectedItem = item;
    }
}
