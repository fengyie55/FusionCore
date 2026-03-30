using FusionApp.Composition;
using FusionLog.Entries;
using FusionStudio.Layout;
using FusionStudio.Models;
using FusionStudio.Navigation;
using FusionStudio.Projections;
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
        var runtimeSummary = new StudioRuntimeSummaryModel(
            "FusionStudio",
            "Constructed",
            "Prepared",
            "studio-demo",
            "Development",
            @"R:\FusionRuntime\DemoEquipment",
            [
                new StudioModuleSummaryModel("LP01", "LoadPort-01", "Idle"),
                new StudioModuleSummaryModel("TM01", "TransferModule-01", "Ready"),
                new StudioModuleSummaryModel("PM01", "ProcessModule-01", "Standby")
            ]);
        var configurationSummary = new StudioConfigurationSummaryModel(
            false,
            @"R:\FusionRuntime\DemoEquipment\config",
            "当前使用默认工程配置摘要示例。");
        var logSummary = StudioLogSummaryModel.Empty;
        var deviceOverview = CreateDeviceOverview(
            "FusionCore Demo Equipment",
            runtimeSummary,
            configurationSummary);
        var moduleContexts = CreateModuleContexts(
            runtimeSummary.Profile,
            runtimeSummary.RuntimeRoot,
            deviceOverview.Modules);

        return new StudioBootstrapContext(
            new StudioShellOptions(
                "FusionStudio",
                "设备工程配置与调试工作台",
                "当前提供设备总览、模块工作台、报警/互锁/IO 与工程调试入口骨架。"),
            CreateDefaultNavigationOptions(),
            new StudioRuntimeDescriptor(
                deviceOverview.EquipmentName,
                runtimeSummary.Profile,
                runtimeSummary.RuntimeRoot,
                CreateDefaultDependencies()),
            configurationSummary,
            runtimeSummary,
            logSummary,
            deviceOverview,
            deviceOverview.EngineeringTree,
            moduleContexts);
    }

    /// <summary>
    /// 从应用装配结果创建工程工作台上下文。
    /// </summary>
    public static StudioBootstrapContext CreateBootstrapContext(
        ApplicationAssembly assembly,
        IReadOnlyCollection<LogEntry>? logEntries = null)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var runtimeSummary = StudioRuntimeProjection.FromDiagnostic(assembly.Runtime.Host.DiagnosticInfo);
        var configurationSummary = StudioConfigurationProjection.FromAssembly(assembly);
        var logSummary = StudioLogProjection.FromEntries(logEntries);
        var deviceOverview = CreateDeviceOverview(
            assembly.RuntimeDescriptor.DisplayTitle,
            runtimeSummary,
            configurationSummary);
        var moduleContexts = CreateModuleContexts(
            runtimeSummary.Profile,
            runtimeSummary.RuntimeRoot,
            deviceOverview.Modules);

        return new StudioBootstrapContext(
            new StudioShellOptions(
                assembly.StudioBootstrapDescriptor.DisplayTitle,
                "设备工程配置与调试工作台",
                assembly.StudioBootstrapDescriptor.StartupMessage),
            CreateDefaultNavigationOptions(),
            new StudioRuntimeDescriptor(
                assembly.StudioBootstrapDescriptor.DisplayTitle,
                runtimeSummary.Profile,
                runtimeSummary.RuntimeRoot,
                CreateDependencies(assembly)),
            configurationSummary,
            runtimeSummary,
            logSummary,
            deviceOverview,
            deviceOverview.EngineeringTree,
            moduleContexts);
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
            context.LogSummary,
            context.DeviceOverview,
            context.EngineeringTree,
            context.ModuleContexts);

        var firstRoute = StudioRoute.DeviceOverview;
        var firstItem = shell.Navigation.Sections
            .SelectMany(section => section.Items)
            .First(item => item.Route == firstRoute);
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
            "设备工程总览区",
            "工程树导航区",
            "模块工作区",
            "状态与调试摘要区");
    }

    private static StudioNavigationOptions CreateDefaultNavigationOptions()
    {
        return new StudioNavigationOptions(
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true);
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
                new StudioStatusItem("Profile", context.RuntimeSummary.Profile),
                new StudioStatusItem("RuntimeRoot", context.RuntimeSummary.RuntimeRoot),
                new StudioStatusItem("模块数", context.DeviceOverview.Modules.Count.ToString())
            },
            context.ShellOptions.StartupMessage);
    }

    private static StudioDeviceOverviewModel CreateDeviceOverview(
        string equipmentName,
        StudioRuntimeSummaryModel runtimeSummary,
        StudioConfigurationSummaryModel configurationSummary)
    {
        return new StudioDeviceOverviewModel(
            equipmentName,
            configurationSummary.IsConfigurationAvailable
                ? "当前已接入工程配置摘要，可继续承接模块级配置与工程树入口。"
                : "当前尚未接入完整工程配置，仅提供运行与模块摘要入口。",
            runtimeSummary.Profile,
            runtimeSummary.RuntimeRoot,
            CreateModuleNodes(runtimeSummary.Modules),
            CreateEngineeringTree(
                equipmentName,
                runtimeSummary.Profile,
                runtimeSummary.RuntimeRoot,
                runtimeSummary.Modules));
    }

    private static IReadOnlyCollection<StudioModuleNodeModel> CreateModuleNodes(
        IReadOnlyCollection<StudioModuleSummaryModel> modules)
    {
        return modules
            .Select(module => new StudioModuleNodeModel(
                module.ModuleId,
                module.ModuleName,
                ResolveModuleType(module.ModuleName),
                module.State,
                "参数、IO、报警、互锁、状态与调试入口按模块聚合。",
                CreateDefaultToolEntries(module.ModuleName)))
            .ToArray();
    }

    private static IReadOnlyCollection<StudioModuleContextModel> CreateModuleContexts(
        string runtimeProfile,
        string runtimeRoot,
        IReadOnlyCollection<StudioModuleNodeModel> modules)
    {
        return modules
            .OrderBy(module => module.ModuleId, StringComparer.OrdinalIgnoreCase)
            .Select(module => new StudioModuleContextModel(
                module.ModuleId,
                module.ModuleName,
                module.ModuleType,
                module.ModuleState,
                runtimeProfile,
                runtimeRoot,
                "由设备总览模块节点与运行摘要派生。"))
            .ToArray();
    }

    private static StudioEngineeringTreeModel CreateEngineeringTree(
        string equipmentName,
        string runtimeProfile,
        string runtimeRoot,
        IReadOnlyCollection<StudioModuleSummaryModel> modules)
    {
        var moduleNodes = modules
            .Select(module => new StudioEngineeringNodeModel(
                module.ModuleId,
                module.ModuleName,
                StudioEngineeringNodeKind.Module,
                $"{ResolveModuleType(module.ModuleName)} / {module.State}",
                module.State,
                StudioRoute.ModuleWorkbench,
                CreateToolTreeNodes(module)))
            .ToArray();

        return new StudioEngineeringTreeModel(
            equipmentName,
            [
                new StudioEngineeringNodeModel(
                    "Device",
                    equipmentName,
                    StudioEngineeringNodeKind.Device,
                    $"Profile: {runtimeProfile} / RuntimeRoot: {runtimeRoot}",
                    "Online",
                    StudioRoute.DeviceOverview,
                    moduleNodes)
            ]);
    }

    private static IReadOnlyCollection<StudioEngineeringNodeModel> CreateToolTreeNodes(
        StudioModuleSummaryModel module)
    {
        return
        [
            new StudioEngineeringNodeModel(
                $"{module.ModuleId}.Parameters",
                "参数",
                StudioEngineeringNodeKind.Parameters,
                $"{module.ModuleName} 的参数与工程配置入口。",
                null,
                StudioRoute.ConfigurationWorkbench,
                Array.Empty<StudioEngineeringNodeModel>()),
            new StudioEngineeringNodeModel(
                $"{module.ModuleId}.Io",
                "IO",
                StudioEngineeringNodeKind.Io,
                $"{module.ModuleName} 的 IO 摘要与监控入口。",
                null,
                StudioRoute.IoMonitor,
                Array.Empty<StudioEngineeringNodeModel>()),
            new StudioEngineeringNodeModel(
                $"{module.ModuleId}.Alarms",
                "报警",
                StudioEngineeringNodeKind.Alarms,
                $"{module.ModuleName} 的报警定义与映射入口。",
                null,
                StudioRoute.AlarmConfiguration,
                Array.Empty<StudioEngineeringNodeModel>()),
            new StudioEngineeringNodeModel(
                $"{module.ModuleId}.Interlocks",
                "互锁",
                StudioEngineeringNodeKind.Interlocks,
                $"{module.ModuleName} 的互锁与工程约束入口。",
                null,
                StudioRoute.InterlockManagement,
                Array.Empty<StudioEngineeringNodeModel>()),
            new StudioEngineeringNodeModel(
                $"{module.ModuleId}.State",
                "状态",
                StudioEngineeringNodeKind.State,
                $"{module.ModuleName} 的运行状态与诊断摘要。",
                module.State,
                StudioRoute.RuntimeDiagnostics,
                Array.Empty<StudioEngineeringNodeModel>()),
            new StudioEngineeringNodeModel(
                $"{module.ModuleId}.Debug",
                "调试",
                StudioEngineeringNodeKind.Debug,
                $"{module.ModuleName} 的工程调试与控制入口。",
                null,
                StudioRoute.ControlConsole,
                Array.Empty<StudioEngineeringNodeModel>())
        ];
    }

    private static IReadOnlyCollection<StudioModuleToolEntryModel> CreateDefaultToolEntries(string moduleName)
    {
        return
        [
            new StudioModuleToolEntryModel("Parameters", "参数", $"{moduleName} 的参数与工程配置入口。"),
            new StudioModuleToolEntryModel("Io", "IO", $"{moduleName} 的 IO 摘要与监控入口。"),
            new StudioModuleToolEntryModel("Alarms", "报警", $"{moduleName} 的报警定义与映射入口。"),
            new StudioModuleToolEntryModel("Interlocks", "互锁", $"{moduleName} 的互锁与工程约束入口。"),
            new StudioModuleToolEntryModel("State", "状态", $"{moduleName} 的运行状态与诊断摘要。"),
            new StudioModuleToolEntryModel("Debug", "调试", $"{moduleName} 的工程调试与控制入口。")
        ];
    }

    private static string ResolveModuleType(string moduleName)
    {
        if (moduleName.Contains("LoadPort", StringComparison.OrdinalIgnoreCase))
        {
            return "LoadPort";
        }

        if (moduleName.Contains("Transfer", StringComparison.OrdinalIgnoreCase))
        {
            return "Robot";
        }

        if (moduleName.Contains("Process", StringComparison.OrdinalIgnoreCase))
        {
            return "ProcessModule";
        }

        return "Module";
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

    private static IReadOnlyCollection<StudioDependencyDescriptor> CreateDependencies(ApplicationAssembly assembly)
    {
        return
        [
            new StudioDependencyDescriptor(
                "FusionKernel",
                true,
                "应用装配结果已提供宿主与模块运行摘要。"),
            new StudioDependencyDescriptor(
                "FusionConfig",
                assembly.Boundary.ConfigurationProvider is not null || assembly.Boundary.ConfigurationSnapshot is not null,
                assembly.Boundary.ConfigurationProvider is null && assembly.Boundary.ConfigurationSnapshot is null
                    ? "当前尚未注入配置边界。"
                    : "应用装配结果已提供最小配置读取边界。"),
            new StudioDependencyDescriptor(
                "FusionLog",
                assembly.Boundary.LoggerWriter is not null || assembly.Boundary.LoggerContext is not null,
                assembly.Boundary.LoggerWriter is null && assembly.Boundary.LoggerContext is null
                    ? "当前尚未注入日志边界。"
                    : "应用装配结果已提供最小日志边界。"),
            new StudioDependencyDescriptor(
                "FusionApp",
                true,
                "当前工作台由应用装配结果驱动默认接线。")
        ];
    }
}
