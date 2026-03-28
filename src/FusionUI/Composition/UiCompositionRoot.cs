using FusionUI.Layout;
using FusionUI.Models;
using FusionUI.Navigation;
using FusionUI.Projections;
using FusionUI.Shell;
using FusionApp.Composition;

namespace FusionUI.Composition;

/// <summary>
/// 提供 UI 壳层的最小组合入口。
/// </summary>
public static class UiCompositionRoot
{
    /// <summary>
    /// 从应用装配结果创建最小 Shell 视图模型。
    /// </summary>
    public static ShellViewModel CreateShell(ApplicationAssembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return CreateShell(UiApplicationProjection.CreateBootstrapContext(assembly));
    }

    /// <summary>
    /// 创建最小 Shell 视图模型。
    /// </summary>
    public static ShellViewModel CreateShell(UiBootstrapContext? bootstrapContext = null)
    {
        var mappingResult = bootstrapContext?.MappingResult ?? UiOptionsBinder.Bind(null);
        var runtimeSummary = bootstrapContext?.RuntimeSummary ?? RuntimeSummaryModel.Empty;
        var logsProjection = bootstrapContext?.LogsProjection ?? LogsViewProjection.Empty;
        var layout = CreateLayoutDescriptor();
        var navigation = CreateNavigationViewModel(mappingResult.NavigationOptions);

        var shell = new ShellViewModel(
            mappingResult.ShellOptions,
            mappingResult.StatusBarOptions,
            layout,
            navigation,
            runtimeSummary,
            logsProjection);

        var firstItem = navigation.Sections.SelectMany(section => section.Items).First();
        shell.NavigateTo(firstItem);
        return shell;
    }

    /// <summary>
    /// 创建 UI 运行态描述信息。
    /// </summary>
    public static UiRuntimeDescriptor CreateRuntimeDescriptor(UiBootstrapContext? bootstrapContext = null)
    {
        var mappingResult = bootstrapContext?.MappingResult ?? UiOptionsBinder.Bind(null);
        var runtimeSummary = bootstrapContext?.RuntimeSummary ?? RuntimeSummaryModel.Empty;
        var layout = CreateLayoutDescriptor();
        var navigation = CreateNavigationViewModel(mappingResult.NavigationOptions);
        var dependencies = bootstrapContext?.Dependencies ?? CreateDefaultDependencies();

        return new UiRuntimeDescriptor(
            mappingResult.ShellOptions.ApplicationTitle,
            layout,
            navigation.Sections.Select(section => section.Title).ToList(),
            runtimeSummary,
            dependencies);
    }

    /// <summary>
    /// 从应用装配结果创建 UI 运行描述。
    /// </summary>
    public static UiRuntimeDescriptor CreateRuntimeDescriptor(ApplicationAssembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        return CreateRuntimeDescriptor(UiApplicationProjection.CreateBootstrapContext(assembly));
    }

    private static ShellLayoutDescriptor CreateLayoutDescriptor()
    {
        return new ShellLayoutDescriptor(
            "顶部状态区",
            "导航区",
            "工作区",
            "状态摘要区");
    }

    private static NavigationViewModel CreateNavigationViewModel(UiNavigationOptions options)
    {
        var primaryItems = new List<NavigationItem>
        {
            new(UiRoute.Overview, "概览", "设备与系统概览入口。", "主视图"),
            new(UiRoute.Operator, "操作员", "操作员工作区入口。", "主视图"),
            new(UiRoute.Engineer, "工程师", "工程师工作区入口。", "主视图")
        };

        var platformItems = new List<NavigationItem>();

        if (options.IncludeRuntimeEntry)
        {
            platformItems.Add(new NavigationItem(UiRoute.Runtime, "运行", "宿主与运行状态只读入口。", "平台"));
        }

        if (options.IncludeLogsEntry)
        {
            platformItems.Add(new NavigationItem(UiRoute.Logs, "日志", "日志摘要展示入口占位。", "平台"));
        }

        if (options.IncludeEquipmentEntry)
        {
            platformItems.Add(new NavigationItem(UiRoute.Equipment, "设备", "设备与模块页面入口。", "平台"));
        }

        var sections = new List<NavigationSection>
        {
            new("主视图", primaryItems)
        };

        if (platformItems.Count > 0)
        {
            sections.Add(new NavigationSection("平台", platformItems));
        }

        return new NavigationViewModel(sections);
    }

    private static IReadOnlyCollection<UiDependencyDescriptor> CreateDefaultDependencies()
    {
        return
        [
            new UiDependencyDescriptor("Config", false, "当前尚未注入 UI 配置快照。"),
            new UiDependencyDescriptor("Runtime", false, "当前尚未注入宿主运行态摘要。"),
            new UiDependencyDescriptor("Logs", false, "当前尚未注入日志摘要入口。")
        ];
    }
}
