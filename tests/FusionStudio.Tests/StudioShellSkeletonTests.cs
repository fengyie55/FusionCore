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
    public void Navigation_Can_Switch_To_Control_Console()
    {
        var shell = StudioCompositionRoot.CreateShell();
        var item = shell.Navigation.Sections
            .SelectMany(section => section.Items)
            .Single(entry => entry.Route == StudioRoute.ControlConsole);

        shell.NavigateTo(item);

        Assert.Equal("工程控制台", shell.CurrentViewTitle);
        Assert.IsType<ControlConsoleViewModel>(shell.CurrentViewModel);
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
    }

    [Fact]
    public void Log_Projection_Can_Create_Readonly_Summary()
    {
        var summary = StudioLogProjection.FromEntries([CreateLogEntry()]);

        Assert.Single(summary.Entries);
        Assert.Contains("1", summary.SummaryText);
        Assert.Equal("FusionKernel.PlatformModule", summary.Entries.Single().Source);
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
