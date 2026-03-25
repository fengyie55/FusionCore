using FusionUI.Composition;
using FusionUI.Layout;
using FusionUI.Models;
using FusionUI.Navigation;
using FusionUI.Projections;
using FusionUI.ViewModels;

namespace FusionUI.Shell;

/// <summary>
/// 表示 UI Shell 的最小视图模型。
/// </summary>
public sealed class ShellViewModel : ObservableObject
{
    private object? _currentViewModel;
    private string _currentViewTitle = string.Empty;
    private StatusBarModel _statusBar = StatusBarModel.Empty;
    private readonly UiStatusBarOptions _statusBarOptions;

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
    /// 当前运行态摘要。
    /// </summary>
    public RuntimeSummaryModel RuntimeSummary { get; }

    /// <summary>
    /// 当前日志入口摘要。
    /// </summary>
    public LogsViewProjection LogsProjection { get; }

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
    /// 当前状态栏模型。
    /// </summary>
    public StatusBarModel StatusBar
    {
        get => _statusBar;
        private set => SetProperty(ref _statusBar, value);
    }

    /// <summary>
    /// 初始化 Shell 视图模型。
    /// </summary>
    public ShellViewModel(
        UiShellOptions shellOptions,
        UiStatusBarOptions statusBarOptions,
        ShellLayoutDescriptor layout,
        NavigationViewModel navigation,
        RuntimeSummaryModel runtimeSummary,
        LogsViewProjection logsProjection)
    {
        ApplicationTitle = shellOptions.ApplicationTitle;
        ShellSubtitle = shellOptions.ShellSubtitle;
        Layout = layout;
        Navigation = navigation;
        RuntimeSummary = runtimeSummary;
        LogsProjection = logsProjection;
        _statusBarOptions = statusBarOptions;
        _currentViewTitle = shellOptions.ApplicationTitle;
        _statusBar = CreateStatusBar(shellOptions.ApplicationTitle, shellOptions.StartupMessage);
    }

    /// <summary>
    /// 导航到指定页面。
    /// </summary>
    public void NavigateTo(NavigationItem item)
    {
        Navigation.Select(item);
        CurrentViewModel = CreateViewModel(item.Route);
        CurrentViewTitle = item.Title;
        StatusBar = CreateStatusBar(item.Title, item.Description);
    }

    private object CreateViewModel(UiRoute route)
    {
        return route switch
        {
            UiRoute.Overview => new OverviewViewModel(),
            UiRoute.Operator => new OperatorViewModel(),
            UiRoute.Engineer => new EngineerViewModel(),
            UiRoute.Runtime => new RuntimeViewModel(RuntimeSummary),
            UiRoute.Logs => new LogsViewModel(LogsProjection),
            UiRoute.Equipment => new EquipmentViewModel(),
            _ => new OverviewViewModel()
        };
    }

    private StatusBarModel CreateStatusBar(string currentPage, string messageText)
    {
        var items = new List<StatusBarItem>();

        if (_statusBarOptions.ShowCurrentPage)
        {
            items.Add(new StatusBarItem("页面", currentPage));
        }

        if (_statusBarOptions.ShowHostState)
        {
            items.Add(new StatusBarItem("宿主", RuntimeSummary.Host.HostState));
        }

        if (_statusBarOptions.ShowProfile)
        {
            items.Add(new StatusBarItem("Profile", RuntimeSummary.Host.Profile ?? "n/a"));
        }

        if (_statusBarOptions.ShowRuntimeRoot)
        {
            items.Add(new StatusBarItem("运行根", RuntimeSummary.Host.RuntimeRoot));
        }

        return new StatusBarModel(items, new UiStatusMessage(messageText));
    }
}
