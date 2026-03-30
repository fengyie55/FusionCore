using FusionStudio.Layout;
using FusionStudio.Models;
using FusionStudio.Navigation;
using FusionStudio.Shell;

namespace FusionStudio.Composition;

/// <summary>
/// 提供 FusionStudio 的最小工程工作台组合入口。
/// </summary>
public static class StudioCompositionRoot
{
    /// <summary>
    /// 创建默认工程工作台上下文。
    /// </summary>
    public static StudioBootstrapContext CreateBootstrapContext()
    {
        var modules = CreateDefaultModules();
        var deviceOverview = new StudioDeviceOverviewModel(
            "FusionCore Demo Equipment",
            "面向设备厂家、开发工程师与现场调试工程师的工程工作台总览。",
            "Development",
            "R:\\FusionRuntime\\DemoEquipment",
            modules);

        return new StudioBootstrapContext(
            new StudioShellOptions(
                "FusionStudio",
                "设备工程配置与调试工作台",
                "当前提供设备总览、模块工作台、报警/互锁/IO 与工程调试入口骨架。"),
            new StudioNavigationOptions(
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true),
            new StudioRuntimeDescriptor(
                deviceOverview.EquipmentName,
                deviceOverview.RuntimeProfile,
                deviceOverview.RuntimeRoot,
                CreateDefaultDependencies()),
            deviceOverview);
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
            context.DeviceOverview);

        var firstItem = shell.Navigation.Sections.SelectMany(section => section.Items).First();
        shell.NavigateTo(firstItem);
        return shell;
    }

    private static StudioLayoutDescriptor CreateLayoutDescriptor()
    {
        return new StudioLayoutDescriptor(
            "设备工程总览区",
            "工程树导航区",
            "模块工作区",
            "状态与调试摘要区");
    }

    private static StudioNavigationViewModel CreateNavigation(StudioNavigationOptions options)
    {
        var overviewItems = new List<NavigationItem>
        {
            new(
                StudioRoute.DeviceOverview,
                "设备总览",
                "显示整机摘要、模块树和工程工具入口。",
                "设备总览")
        };

        var configurationItems = new List<NavigationItem>();
        if (options.IncludeConfigurationEntry)
        {
            configurationItems.Add(new NavigationItem(
                StudioRoute.ConfigurationWorkbench,
                "工程配置",
                "用于查看整机、模块与运行实例配置范围。",
                "工程配置"));
        }

        if (options.IncludeAlarmEntry)
        {
            configurationItems.Add(new NavigationItem(
                StudioRoute.AlarmConfiguration,
                "报警配置",
                "用于管理报警定义、报警映射与模块报警范围的入口占位。",
                "工程配置"));
        }

        if (options.IncludeInterlockEntry)
        {
            configurationItems.Add(new NavigationItem(
                StudioRoute.InterlockManagement,
                "互锁管理",
                "用于查看跨模块互锁、工艺互锁与调试互锁的入口占位。",
                "工程配置"));
        }

        var moduleItems = new List<NavigationItem>();
        if (options.IncludeModuleWorkbenchEntry)
        {
            moduleItems.Add(new NavigationItem(
                StudioRoute.ModuleWorkbench,
                "模块工作台",
                "按模块组织参数、状态、报警、互锁、IO 与调试入口。",
                "模块工作台"));
        }

        if (options.IncludeIoMonitorEntry)
        {
            moduleItems.Add(new NavigationItem(
                StudioRoute.IoMonitor,
                "IO 监控",
                "用于观察模块 IO 摘要、信号入口与联调位。",
                "模块工作台"));
        }

        var diagnosticsItems = new List<NavigationItem>();
        if (options.IncludeRuntimeDiagnosticsEntry)
        {
            diagnosticsItems.Add(new NavigationItem(
                StudioRoute.RuntimeDiagnostics,
                "运行诊断",
                "用于查看宿主、运行实例与模块状态摘要。",
                "监控与诊断"));
        }

        if (options.IncludeLogsEntry)
        {
            diagnosticsItems.Add(new NavigationItem(
                StudioRoute.LogsWorkbench,
                "详细日志",
                "用于承载整机与模块日志入口、故障追踪入口与联调摘要。",
                "监控与诊断"));
        }

        var controlItems = new List<NavigationItem>();
        if (options.IncludeControlConsoleEntry)
        {
            controlItems.Add(new NavigationItem(
                StudioRoute.ControlConsole,
                "工程控制台",
                "用于承载模块工程指令、手动测试与控制入口。",
                "工程控制"));
        }

        if (options.IncludeDebugAssistantEntry)
        {
            controlItems.Add(new NavigationItem(
                StudioRoute.DebugAssistant,
                "调试助手",
                "用于问题定位、联调提示与后续工程工具扩展入口。",
                "工程控制"));
        }

        return new StudioNavigationViewModel(
        [
            new NavigationSection("设备总览", overviewItems),
            new NavigationSection("工程配置", configurationItems),
            new NavigationSection("模块工作台", moduleItems),
            new NavigationSection("监控与诊断", diagnosticsItems),
            new NavigationSection("工程控制", controlItems)
        ]);
    }

    private static StudioStatusModel CreateStatusModel(StudioBootstrapContext context)
    {
        return new StudioStatusModel(
            new[]
            {
                new StudioStatusItem("Profile", context.RuntimeDescriptor.CurrentProfile),
                new StudioStatusItem("RuntimeRoot", context.RuntimeDescriptor.RuntimeRootSummary),
                new StudioStatusItem("模块数", context.DeviceOverview.Modules.Count.ToString())
            },
            context.ShellOptions.StartupMessage);
    }

    private static IReadOnlyCollection<StudioDependencyDescriptor> CreateDefaultDependencies()
    {
        return
        [
            new StudioDependencyDescriptor("FusionKernel", false, "当前只消费宿主与运行时摘要边界。"),
            new StudioDependencyDescriptor("FusionConfig", false, "当前只消费工程配置摘要与 section 映射结果。"),
            new StudioDependencyDescriptor("FusionLog", false, "当前只消费日志入口摘要，不实现日志平台。"),
            new StudioDependencyDescriptor("FusionApp", false, "当前由应用层负责后续默认装配。")
        ];
    }

    private static IReadOnlyCollection<StudioModuleNodeModel> CreateDefaultModules()
    {
        return
        [
            new StudioModuleNodeModel(
                "LP01",
                "LoadPort-01",
                "LoadPort",
                "Idle",
                ["参数", "IO", "报警", "互锁", "状态", "调试"]),
            new StudioModuleNodeModel(
                "TM01",
                "TransferModule-01",
                "Robot",
                "Ready",
                ["参数", "IO", "报警", "互锁", "状态", "控制"]),
            new StudioModuleNodeModel(
                "PM01",
                "ProcessModule-01",
                "ProcessModule",
                "Standby",
                ["参数", "IO", "报警", "互锁", "状态", "日志"])
        ];
    }
}
