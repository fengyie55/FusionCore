using FusionUI.Layout;
using FusionUI.Navigation;
using FusionUI.Shell;

namespace FusionUI.Composition;

/// <summary>
/// 提供 UI 壳层的最小组合入口。
/// </summary>
public static class UiCompositionRoot
{
    /// <summary>
    /// 创建最小 Shell 视图模型。
    /// </summary>
    public static ShellViewModel CreateShell(
        UiShellOptions? shellOptions = null,
        UiNavigationOptions? navigationOptions = null)
    {
        var resolvedShellOptions = shellOptions ?? CreateDefaultShellOptions();
        var resolvedNavigationOptions = navigationOptions ?? new UiNavigationOptions();
        var layout = CreateLayoutDescriptor();
        var navigation = CreateNavigationViewModel(resolvedNavigationOptions);
        var shell = new ShellViewModel(resolvedShellOptions, layout, navigation);
        var firstItem = navigation.Sections.SelectMany(section => section.Items).First();
        shell.NavigateTo(firstItem);
        return shell;
    }

    /// <summary>
    /// 创建 UI 运行时描述信息。
    /// </summary>
    public static UiRuntimeDescriptor CreateRuntimeDescriptor(UiNavigationOptions? navigationOptions = null)
    {
        var layout = CreateLayoutDescriptor();
        var navigation = CreateNavigationViewModel(navigationOptions ?? new UiNavigationOptions());
        return new UiRuntimeDescriptor(
            CreateDefaultShellOptions().ApplicationTitle,
            layout,
            navigation.Sections.Select(section => section.Title).ToList());
    }

    private static UiShellOptions CreateDefaultShellOptions()
    {
        return new UiShellOptions(
            "FusionCore",
            "E95 对齐的最小操作界面壳层",
            "当前阶段仅提供壳层、导航和占位视图。");
    }

    private static ShellLayoutDescriptor CreateLayoutDescriptor()
    {
        return new ShellLayoutDescriptor(
            "顶部状态区",
            "导航区",
            "工作区",
            "状态消息区");
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
            platformItems.Add(new NavigationItem(UiRoute.Runtime, "运行", "宿主与运行状态入口。", "平台"));
        }

        if (options.IncludeLogsEntry)
        {
            platformItems.Add(new NavigationItem(UiRoute.Logs, "日志", "日志显示入口占位。", "平台"));
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
}
