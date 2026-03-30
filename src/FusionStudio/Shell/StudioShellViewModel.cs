using FusionStudio.Composition;
using FusionStudio.Layout;
using FusionStudio.Models;
using FusionStudio.Navigation;
using FusionStudio.ViewModels;

namespace FusionStudio.Shell;

/// <summary>
/// 表示 FusionStudio 壳层的最小视图模型。
/// </summary>
public sealed class StudioShellViewModel : ObservableObject
{
    private object? _currentViewModel;
    private string _currentViewTitle = string.Empty;
    private StudioStatusModel _status = StudioStatusModel.Empty;

    /// <summary>
    /// 获取应用标题。
    /// </summary>
    public string ApplicationTitle { get; }

    /// <summary>
    /// 获取壳层副标题。
    /// </summary>
    public string ShellSubtitle { get; }

    /// <summary>
    /// 获取布局语义。
    /// </summary>
    public StudioLayoutDescriptor Layout { get; }

    /// <summary>
    /// 获取导航视图模型。
    /// </summary>
    public StudioNavigationViewModel Navigation { get; }

    /// <summary>
    /// 获取运行摘要。
    /// </summary>
    public StudioRuntimeDescriptor RuntimeDescriptor { get; }

    /// <summary>
    /// 获取设备总览摘要。
    /// </summary>
    public StudioDeviceOverviewModel DeviceOverview { get; }

    /// <summary>
    /// 获取模块树节点集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleNodeModel> Modules => DeviceOverview.Modules;

    /// <summary>
    /// 获取当前工作区标题。
    /// </summary>
    public string CurrentViewTitle
    {
        get => _currentViewTitle;
        private set => SetProperty(ref _currentViewTitle, value);
    }

    /// <summary>
    /// 获取当前工作区视图模型。
    /// </summary>
    public object? CurrentViewModel
    {
        get => _currentViewModel;
        private set => SetProperty(ref _currentViewModel, value);
    }

    /// <summary>
    /// 获取状态栏模型。
    /// </summary>
    public StudioStatusModel Status
    {
        get => _status;
        private set => SetProperty(ref _status, value);
    }

    /// <summary>
    /// 初始化工作台壳层视图模型。
    /// </summary>
    public StudioShellViewModel(
        StudioShellOptions shellOptions,
        StudioLayoutDescriptor layout,
        StudioNavigationViewModel navigation,
        StudioStatusModel status,
        StudioRuntimeDescriptor runtimeDescriptor,
        StudioDeviceOverviewModel deviceOverview)
    {
        ApplicationTitle = shellOptions.ApplicationTitle;
        ShellSubtitle = shellOptions.ShellSubtitle;
        Layout = layout;
        Navigation = navigation;
        RuntimeDescriptor = runtimeDescriptor;
        DeviceOverview = deviceOverview;
        _status = status;
        _currentViewTitle = shellOptions.ApplicationTitle;
    }

    /// <summary>
    /// 导航到指定页面。
    /// </summary>
    public void NavigateTo(NavigationItem item)
    {
        Navigation.Select(item);
        CurrentViewTitle = item.Title;
        CurrentViewModel = item.Route switch
        {
            StudioRoute.DeviceOverview => new DeviceOverviewViewModel(DeviceOverview),
            StudioRoute.ConfigurationWorkbench => new ConfigurationWorkbenchViewModel(DeviceOverview),
            StudioRoute.AlarmConfiguration => new AlarmConfigurationViewModel(Modules),
            StudioRoute.InterlockManagement => new InterlockManagementViewModel(Modules),
            StudioRoute.ModuleWorkbench => new ModuleWorkbenchViewModel(Modules),
            StudioRoute.IoMonitor => new IoMonitorViewModel(Modules),
            StudioRoute.RuntimeDiagnostics => new RuntimeDiagnosticsViewModel(DeviceOverview, RuntimeDescriptor),
            StudioRoute.LogsWorkbench => new LogsWorkbenchViewModel(Modules),
            StudioRoute.ControlConsole => new ControlConsoleViewModel(Modules),
            StudioRoute.DebugAssistant => new DebugAssistantViewModel(),
            _ => new DeviceOverviewViewModel(DeviceOverview)
        };

        Status = new StudioStatusModel(
            new[]
            {
                new StudioStatusItem("页面", item.Title),
                new StudioStatusItem("Profile", RuntimeDescriptor.CurrentProfile),
                new StudioStatusItem("RuntimeRoot", RuntimeDescriptor.RuntimeRootSummary),
                new StudioStatusItem("模块数", Modules.Count.ToString())
            },
            item.Description);
    }
}
