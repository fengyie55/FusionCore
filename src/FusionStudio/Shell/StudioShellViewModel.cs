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
    /// 获取运行摘要描述。
    /// </summary>
    public StudioRuntimeDescriptor RuntimeDescriptor { get; }

    /// <summary>
    /// 获取工程配置摘要。
    /// </summary>
    public StudioConfigurationSummaryModel ConfigurationSummary { get; }

    /// <summary>
    /// 获取运行态摘要。
    /// </summary>
    public StudioRuntimeSummaryModel RuntimeSummary { get; }

    /// <summary>
    /// 获取日志摘要。
    /// </summary>
    public StudioLogSummaryModel LogSummary { get; }

    /// <summary>
    /// 获取设备总览摘要。
    /// </summary>
    public StudioDeviceOverviewModel DeviceOverview { get; }

    /// <summary>
    /// 获取工程树只读模型。
    /// </summary>
    public StudioEngineeringTreeModel EngineeringTree { get; }

    /// <summary>
    /// 获取模块上下文集合。
    /// </summary>
    public IReadOnlyCollection<StudioModuleContextModel> ModuleContexts { get; }

    /// <summary>
    /// 获取稳定的默认模块上下文。
    /// </summary>
    public StudioModuleContextModel? DefaultModuleContext { get; }

    /// <summary>
    /// 获取模块节点集合。
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
        StudioConfigurationSummaryModel configurationSummary,
        StudioRuntimeSummaryModel runtimeSummary,
        StudioLogSummaryModel logSummary,
        StudioDeviceOverviewModel deviceOverview,
        StudioEngineeringTreeModel engineeringTree,
        IReadOnlyCollection<StudioModuleContextModel> moduleContexts)
    {
        ApplicationTitle = shellOptions.ApplicationTitle;
        ShellSubtitle = shellOptions.ShellSubtitle;
        Layout = layout;
        Navigation = navigation;
        RuntimeDescriptor = runtimeDescriptor;
        ConfigurationSummary = configurationSummary;
        RuntimeSummary = runtimeSummary;
        LogSummary = logSummary;
        DeviceOverview = deviceOverview;
        EngineeringTree = engineeringTree;
        ModuleContexts = moduleContexts;
        DefaultModuleContext = ModuleContexts
            .OrderBy(item => item.ModuleId, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
        _status = status;
        _currentViewTitle = shellOptions.ApplicationTitle;
    }

    /// <summary>
    /// 为工具页分发统一上下文。
    /// </summary>
    public StudioToolPageContextModel? ResolveToolPageContext(
        StudioRoute route,
        string? moduleId = null)
    {
        var moduleContext = ResolveModuleContext(moduleId);
        if (moduleContext is null)
        {
            return null;
        }

        return new StudioToolPageContextModel(
            DeviceOverview.EquipmentName,
            ResolveToolDomain(route),
            moduleContext,
            $"由路由 {route} 与模块上下文 {moduleContext.ModuleId} 组合生成。");
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
            StudioRoute.ConfigurationWorkbench => new ConfigurationWorkbenchViewModel(DeviceOverview, ConfigurationSummary),
            StudioRoute.AlarmConfiguration => new AlarmConfigurationViewModel(Modules, GetRequiredToolPageContext(StudioRoute.AlarmConfiguration)),
            StudioRoute.InterlockManagement => new InterlockManagementViewModel(Modules, GetRequiredToolPageContext(StudioRoute.InterlockManagement)),
            StudioRoute.ModuleWorkbench => new ModuleWorkbenchViewModel(Modules, EngineeringTree),
            StudioRoute.IoMonitor => new IoMonitorViewModel(Modules, GetRequiredToolPageContext(StudioRoute.IoMonitor)),
            StudioRoute.RuntimeDiagnostics => new RuntimeDiagnosticsViewModel(DeviceOverview, RuntimeSummary),
            StudioRoute.LogsWorkbench => new LogsWorkbenchViewModel(Modules, LogSummary),
            StudioRoute.ControlConsole => new ControlConsoleViewModel(Modules, GetRequiredToolPageContext(StudioRoute.ControlConsole)),
            StudioRoute.DebugAssistant => new DebugAssistantViewModel(),
            _ => new DeviceOverviewViewModel(DeviceOverview)
        };

        var contextLabel = DefaultModuleContext is null
            ? "None"
            : $"{DefaultModuleContext.ModuleName}({DefaultModuleContext.ModuleState})";

        Status = new StudioStatusModel(
            new[]
            {
                new StudioStatusItem("页面", item.Title),
                new StudioStatusItem("Profile", RuntimeSummary.Profile),
                new StudioStatusItem("RuntimeRoot", RuntimeSummary.RuntimeRoot),
                new StudioStatusItem("DefaultModule", contextLabel)
            },
            item.Description);
    }

    private StudioModuleContextModel? ResolveModuleContext(string? moduleId)
    {
        if (string.IsNullOrWhiteSpace(moduleId))
        {
            return DefaultModuleContext;
        }

        return ModuleContexts.FirstOrDefault(
                   item => string.Equals(item.ModuleId, moduleId, StringComparison.OrdinalIgnoreCase))
               ?? DefaultModuleContext;
    }

    private StudioToolPageContextModel GetRequiredToolPageContext(StudioRoute route)
    {
        return ResolveToolPageContext(route) ?? new StudioToolPageContextModel(
            DeviceOverview.EquipmentName,
            ResolveToolDomain(route),
            new StudioModuleContextModel(
                "N/A",
                "UnknownModule",
                "UnknownType",
                "UnknownState",
                RuntimeSummary.Profile,
                RuntimeSummary.RuntimeRoot,
                "当前无可用模块上下文，使用只读占位上下文。"),
            $"由路由 {route} 生成占位工具页上下文。");
    }

    private static StudioToolDomain ResolveToolDomain(StudioRoute route)
    {
        return route switch
        {
            StudioRoute.DeviceOverview => StudioToolDomain.Overview,
            StudioRoute.ConfigurationWorkbench => StudioToolDomain.Configuration,
            StudioRoute.AlarmConfiguration => StudioToolDomain.Alarm,
            StudioRoute.InterlockManagement => StudioToolDomain.Interlock,
            StudioRoute.ModuleWorkbench => StudioToolDomain.Module,
            StudioRoute.IoMonitor => StudioToolDomain.Io,
            StudioRoute.RuntimeDiagnostics => StudioToolDomain.Runtime,
            StudioRoute.LogsWorkbench => StudioToolDomain.Logs,
            StudioRoute.ControlConsole => StudioToolDomain.Control,
            StudioRoute.DebugAssistant => StudioToolDomain.Debug,
            _ => StudioToolDomain.Module
        };
    }
}
