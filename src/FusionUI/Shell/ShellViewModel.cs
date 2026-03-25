using FusionUI.Composition;
using FusionUI.Layout;
using FusionUI.Navigation;
using FusionUI.ViewModels;

namespace FusionUI.Shell;

/// <summary>
/// 表示 UI Shell 的最小视图模型。
/// </summary>
public sealed class ShellViewModel : ObservableObject
{
    private object? _currentViewModel;
    private string _currentViewTitle = string.Empty;
    private string _footerMessage = string.Empty;

    /// <summary>
    /// 应用标题。
    /// </summary>
    public string ApplicationTitle { get; }

    /// <summary>
    /// 壳层副标题。
    /// </summary>
    public string ShellSubtitle { get; }

    /// <summary>
    /// 当前布局描述。
    /// </summary>
    public ShellLayoutDescriptor Layout { get; }

    /// <summary>
    /// 当前导航视图模型。
    /// </summary>
    public NavigationViewModel Navigation { get; }

    /// <summary>
    /// 当前工作区视图模型。
    /// </summary>
    public object? CurrentViewModel
    {
        get => _currentViewModel;
        private set => SetProperty(ref _currentViewModel, value);
    }

    /// <summary>
    /// 当前工作区标题。
    /// </summary>
    public string CurrentViewTitle
    {
        get => _currentViewTitle;
        private set => SetProperty(ref _currentViewTitle, value);
    }

    /// <summary>
    /// 底部消息。
    /// </summary>
    public string FooterMessage
    {
        get => _footerMessage;
        private set => SetProperty(ref _footerMessage, value);
    }

    /// <summary>
    /// 初始化 Shell 视图模型。
    /// </summary>
    public ShellViewModel(
        UiShellOptions shellOptions,
        ShellLayoutDescriptor layout,
        NavigationViewModel navigation)
    {
        ApplicationTitle = shellOptions.ApplicationTitle;
        ShellSubtitle = shellOptions.ShellSubtitle;
        FooterMessage = shellOptions.FooterMessage;
        Layout = layout;
        Navigation = navigation;
        _currentViewTitle = shellOptions.ApplicationTitle;
    }

    /// <summary>
    /// 导航到指定页面。
    /// </summary>
    public void NavigateTo(NavigationItem item)
    {
        Navigation.Select(item);
        CurrentViewModel = CreateViewModel(item.Route);
        CurrentViewTitle = item.Title;
        FooterMessage = item.Description;
    }

    private static object CreateViewModel(UiRoute route)
    {
        return route switch
        {
            UiRoute.Overview => new OverviewViewModel(),
            UiRoute.Operator => new OperatorViewModel(),
            UiRoute.Engineer => new EngineerViewModel(),
            UiRoute.Runtime => new RuntimeViewModel(),
            UiRoute.Logs => new LogsViewModel(),
            UiRoute.Equipment => new EquipmentViewModel(),
            _ => new OverviewViewModel()
        };
    }
}
