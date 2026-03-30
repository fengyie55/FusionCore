using FusionApp.Composition;
using FusionConfig.Profiles;
using FusionConfig.Providers;
using FusionConfig.Runtime;
using FusionConfig.Sections;
using FusionConfig.Snapshots;
using FusionKernel;
using FusionLog.Categories;
using FusionLog.Context;
using FusionLog.Entries;
using FusionLog.Levels;
using FusionStudio.Composition;
using FusionStudio.Models;
using FusionStudio.Navigation;
using FusionStudio.Projections;
using FusionStudio.Shell;
using FusionStudio.ViewModels;

namespace FusionStudio.Tests;

public sealed class StudioShellSkeletonTests
{
    [Fact]
    public void Bootstrap_Context_Can_Be_Created()
    {
        var context = StudioCompositionRoot.CreateBootstrapContext();

        Assert.Equal("FusionStudio", context.ShellOptions.ApplicationTitle);
        Assert.True(context.NavigationOptions.IncludeConfigurationEntry);
        Assert.True(context.NavigationOptions.IncludeAlarmEntry);
        Assert.True(context.NavigationOptions.IncludeModuleWorkbenchEntry);
        Assert.NotEmpty(context.DeviceOverview.Modules);
        Assert.Single(context.EngineeringTree.RootNodes);
        Assert.NotEmpty(context.ModuleContexts);
    }

    [Fact]
    public void Engineering_Tree_Contains_Device_Module_And_Tool_Nodes()
    {
        var context = StudioCompositionRoot.CreateBootstrapContext();

        var deviceNode = Assert.Single(context.EngineeringTree.RootNodes);
        Assert.Equal(StudioEngineeringNodeKind.Device, deviceNode.Kind);
        Assert.NotEmpty(deviceNode.Children);

        var moduleNode = deviceNode.Children.First();
        Assert.Equal(StudioEngineeringNodeKind.Module, moduleNode.Kind);
        Assert.True(moduleNode.Children.Count >= 6);
        Assert.Contains(moduleNode.Children, item => item.Kind == StudioEngineeringNodeKind.Io);
        Assert.Contains(moduleNode.Children, item => item.Kind == StudioEngineeringNodeKind.Alarms);
        Assert.Contains(moduleNode.Children, item => item.Kind == StudioEngineeringNodeKind.Debug);
    }

    [Fact]
    public void Shell_Default_Page_Is_Device_Overview()
    {
        var shell = StudioCompositionRoot.CreateShell();

        Assert.Equal("FusionStudio", shell.ApplicationTitle);
        Assert.NotNull(shell.Navigation);
        Assert.NotNull(shell.Status);
        Assert.IsType<DeviceOverviewViewModel>(shell.CurrentViewModel);
        Assert.Single(shell.EngineeringTree.RootNodes);
    }

    [Fact]
    public void Tool_Pages_Should_Receive_Unified_Context()
    {
        var shell = StudioCompositionRoot.CreateShell();

        var alarmContext = NavigateAndGetToolContext<AlarmConfigurationViewModel>(shell, StudioRoute.AlarmConfiguration);
        var interlockContext = NavigateAndGetToolContext<InterlockManagementViewModel>(shell, StudioRoute.InterlockManagement);
        var ioContext = NavigateAndGetToolContext<IoMonitorViewModel>(shell, StudioRoute.IoMonitor);
        var controlContext = NavigateAndGetToolContext<ControlConsoleViewModel>(shell, StudioRoute.ControlConsole);

        Assert.Equal(alarmContext.EquipmentName, interlockContext.EquipmentName);
        Assert.Equal(alarmContext.EquipmentName, ioContext.EquipmentName);
        Assert.Equal(alarmContext.EquipmentName, controlContext.EquipmentName);

        Assert.Equal(alarmContext.ModuleContext.ModuleId, interlockContext.ModuleContext.ModuleId);
        Assert.Equal(alarmContext.ModuleContext.ModuleId, ioContext.ModuleContext.ModuleId);
        Assert.Equal(alarmContext.ModuleContext.ModuleId, controlContext.ModuleContext.ModuleId);

        Assert.False(string.IsNullOrWhiteSpace(alarmContext.ModuleContext.ModuleName));
        Assert.False(string.IsNullOrWhiteSpace(alarmContext.ModuleContext.ModuleType));
        Assert.False(string.IsNullOrWhiteSpace(alarmContext.ModuleContext.ModuleState));
        Assert.False(string.IsNullOrWhiteSpace(alarmContext.EquipmentName));
    }

    [Fact]
    public void Navigation_Can_Switch_To_Module_Workbench()
    {
        var shell = StudioCompositionRoot.CreateShell();
        var item = shell.Navigation.Sections
            .SelectMany(section => section.Items)
            .Single(entry => entry.Route == StudioRoute.ModuleWorkbench);

        shell.NavigateTo(item);

        var viewModel = Assert.IsType<ModuleWorkbenchViewModel>(shell.CurrentViewModel);
        Assert.NotEmpty(viewModel.EngineeringNodes);
        Assert.Equal("模块工作台", shell.CurrentViewTitle);
    }

    [Fact]
    public void Application_Assembly_Can_Be_Projected_To_Studio_Context()
    {
        var assembly = CreateApplicationAssembly();

        var context = StudioCompositionRoot.CreateBootstrapContext(
            assembly,
            [CreateLogEntry()]);

        Assert.Equal("Development", context.RuntimeSummary.Profile);
        Assert.Equal(@"D:\FusionRuntime", context.RuntimeSummary.RuntimeRoot);
        Assert.True(context.ConfigurationSummary.IsConfigurationAvailable);
        Assert.Equal("DeviceOverview", assembly.StudioBootstrapDescriptor.StartRoute);
        Assert.Single(context.LogSummary.Entries);
        Assert.Single(context.EngineeringTree.RootNodes);
        Assert.NotEmpty(context.ModuleContexts);
    }

    [Fact]
    public void Shell_Can_Be_Created_From_Application_Assembly()
    {
        var assembly = CreateApplicationAssembly();

        var shell = StudioCompositionRoot.CreateShell(
            assembly,
            [CreateLogEntry()]);

        Assert.Equal("FusionStudio", shell.ApplicationTitle);
        Assert.Equal("Development", shell.RuntimeSummary.Profile);
        Assert.Equal(@"D:\FusionRuntime\config", shell.ConfigurationSummary.ConfigRoot);
        Assert.Single(shell.LogSummary.Entries);
        Assert.IsType<DeviceOverviewViewModel>(shell.CurrentViewModel);
        Assert.Single(shell.EngineeringTree.RootNodes);
        Assert.NotEmpty(shell.ModuleContexts);
    }

    [Fact]
    public void Log_Projection_Can_Create_Readonly_Summary()
    {
        var summary = StudioLogProjection.FromEntries([CreateLogEntry()]);

        Assert.Single(summary.Entries);
        Assert.Contains("1", summary.SummaryText);
        Assert.Equal("FusionKernel.PlatformModule", summary.Entries.Single().Source);
    }

    private static StudioToolPageContextModel NavigateAndGetToolContext<TViewModel>(
        StudioShellViewModel shell,
        StudioRoute route)
        where TViewModel : class
    {
        var item = shell.Navigation.Sections
            .SelectMany(section => section.Items)
            .Single(entry => entry.Route == route);

        shell.NavigateTo(item);

        return shell.CurrentViewModel switch
        {
            AlarmConfigurationViewModel alarm when typeof(TViewModel) == typeof(AlarmConfigurationViewModel) => alarm.Context,
            InterlockManagementViewModel interlock when typeof(TViewModel) == typeof(InterlockManagementViewModel) => interlock.Context,
            IoMonitorViewModel io when typeof(TViewModel) == typeof(IoMonitorViewModel) => io.Context,
            ControlConsoleViewModel control when typeof(TViewModel) == typeof(ControlConsoleViewModel) => control.Context,
            _ => throw new InvalidOperationException("当前路由未返回预期的工具页视图模型。")
        };
    }

    private static ApplicationAssembly CreateApplicationAssembly()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(physicalRoot: @"D:\FusionRuntime");
        var snapshot = new ConfigurationSnapshot(
            new EnvironmentProfile("Development", ConfigurationProfileKind.Development, "DEV"),
            runtimeRoot,
            [new UiSection(true, runtimeRoot.ConfigPath)]);
        var provider = new DefaultConfigurationProvider(snapshot);
        var boundary = ApplicationCompositionRoot.CreateBoundary(
            configurationProvider: provider,
            configurationSnapshot: snapshot);
        var bootstrapContext = ApplicationCompositionRoot.CreateBootstrapContext(
            boundary,
            modules: [new PlatformModule()]);

        return ApplicationCompositionRoot.CreateAssembly(bootstrapContext);
    }

    private static LogEntry CreateLogEntry()
    {
        return new LogEntry(
            DateTimeOffset.Parse("2026-03-30T10:00:00+08:00"),
            LogLevel.Information,
            RuntimeLogCategory.Operation,
            new LogMessage("Studio log summary entry"),
            new LogContext(
                new HostLogContext("FusionHost", "FusionHost"),
                new ProcessLogContext("FusionProcess", "FusionStudio"),
                new ModuleLogContext("FusionKernel.PlatformModule", "FusionKernel.PlatformModule", "default")),
            null,
            null,
            Array.Empty<LogProperty>());
    }
}
