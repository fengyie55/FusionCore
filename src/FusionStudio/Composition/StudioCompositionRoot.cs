using FusionApp.Composition;
using FusionLog.Entries;
using FusionStudio.Layout;
using FusionStudio.Models;
using FusionStudio.Navigation;
using FusionStudio.Projections;
using FusionStudio.Shell;

namespace FusionStudio.Composition;

/// <summary>
/// 提供 FusionStudio 的最小组合入口。
/// </summary>
public static class StudioCompositionRoot
{
    /// <summary>
    /// 创建默认接线上下文。
    /// </summary>
    public static StudioBootstrapContext CreateBootstrapContext()
    {
        return new StudioBootstrapContext(
            new StudioShellOptions(
                "FusionStudio",
                "平台工程工作台",
                "当前仅提供工程配置、详细日志、运行诊断与调试入口的只读骨架。"),
            new StudioNavigationOptions(
                true,
                true,
                true,
                true,
                true),
            new StudioRuntimeDescriptor(
                "FusionStudio",
                "未接入",
                "未接入",
                CreateDefaultDependencies()),
            StudioConfigurationSummaryModel.Empty,
            StudioRuntimeSummaryModel.Empty,
            StudioLogSummaryModel.Empty);
    }

    /// <summary>
    /// 从应用装配结果创建默认接线上下文。
    /// </summary>
    public static StudioBootstrapContext CreateBootstrapContext(
        ApplicationAssembly assembly,
        IReadOnlyCollection<LogEntry>? logEntries = null)
    {
        return StudioApplicationProjection.CreateBootstrapContext(assembly, logEntries);
    }

    /// <summary>
    /// 创建工作台壳层视图模型。
    /// </summary>
    public static StudioShellViewModel CreateShell(StudioBootstrapContext? bootstrapContext = null)
    {
        var context = bootstrapContext ?? CreateBootstrapContext();
        var shell = new StudioShellViewModel(
            context.ShellOptions,
            CreateLayoutDescriptor(),
            CreateNavigation(context.NavigationOptions),
            CreateStatusModel(context),
            context.RuntimeDescriptor,
            context.ConfigurationSummary,
            context.RuntimeSummary,
            context.LogSummary);

        var firstItem = shell.Navigation.Sections
            .SelectMany(section => section.Items)
            .First();
        shell.NavigateTo(firstItem);
        return shell;
    }

    /// <summary>
    /// 从应用装配结果创建工作台壳层。
    /// </summary>
    public static StudioShellViewModel CreateShell(
        ApplicationAssembly assembly,
        IReadOnlyCollection<LogEntry>? logEntries = null)
    {
        return CreateShell(CreateBootstrapContext(assembly, logEntries));
    }

    private static StudioLayoutDescriptor CreateLayoutDescriptor()
    {
        return new StudioLayoutDescriptor(
            "工程总览区",
            "工作台导航区",
            "主工作区",
            "状态与接线摘要区");
    }

    private static StudioNavigationViewModel CreateNavigation(StudioNavigationOptions options)
    {
        var workbenchItems = new List<NavigationItem>();

        if (options.IncludeConfigurationEntry)
        {
            workbenchItems.Add(new NavigationItem(
                StudioRoute.ConfigurationWorkbench,
                "工程配置",
                "用于设备工程配置与参数组织入口的只读占位。",
                "工程工作台"));
        }

        if (options.IncludeLogsEntry)
        {
            workbenchItems.Add(new NavigationItem(
                StudioRoute.LogsWorkbench,
                "详细日志",
                "用于日志检视与故障追踪入口的只读占位。",
                "工程工作台"));
        }

        if (options.IncludeRuntimeDiagnosticsEntry)
        {
            workbenchItems.Add(new NavigationItem(
                StudioRoute.RuntimeDiagnostics,
                "运行诊断",
                "用于宿主与模块运行摘要的只读入口。",
                "工程工作台"));
        }

        if (options.IncludeDebugAssistantEntry)
        {
            workbenchItems.Add(new NavigationItem(
                StudioRoute.DebugAssistant,
                "调试助手",
                "用于开发与现场调试辅助入口的占位。",
                "工程工作台"));
        }

        if (options.IncludeModuleExplorerEntry)
        {
            workbenchItems.Add(new NavigationItem(
                StudioRoute.ModuleExplorer,
                "模块状态",
                "用于平台模块摘要与连接状态入口的只读占位。",
                "工程工作台"));
        }

        return new StudioNavigationViewModel(
        [
            new NavigationSection("工程工作台", workbenchItems)
        ]);
    }

    private static StudioStatusModel CreateStatusModel(StudioBootstrapContext context)
    {
        return new StudioStatusModel(
            new[]
            {
                new StudioStatusItem("Profile", context.RuntimeDescriptor.CurrentProfile),
                new StudioStatusItem("RuntimeRoot", context.RuntimeDescriptor.RuntimeRootSummary),
                new StudioStatusItem("依赖数", context.RuntimeDescriptor.Dependencies.Count.ToString())
            },
            context.ShellOptions.StartupMessage);
    }

    private static IReadOnlyCollection<StudioDependencyDescriptor> CreateDefaultDependencies()
    {
        return
        [
            new StudioDependencyDescriptor("FusionKernel", false, "当前仅保留宿主与运行摘要接线位。"),
            new StudioDependencyDescriptor("FusionConfig", false, "当前仅保留工程配置映射接线位。"),
            new StudioDependencyDescriptor("FusionLog", false, "当前仅保留详细日志入口接线位。"),
            new StudioDependencyDescriptor("FusionApp", false, "当前仅保留应用层装配接线位。")
        ];
    }
}
