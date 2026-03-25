using FusionConfig.Sections;
using FusionKernel.Composition;
using FusionKernel.Hosting;
using FusionKernel.Modules;
using FusionKernel.Results;
using FusionKernel.Runtime;
using FusionUI.Composition;
using FusionUI.Models;
using FusionUI.Navigation;
using FusionUI.Projections;
using FusionUI.Shell;
using FusionUI.ViewModels;

namespace FusionUI.Tests;

public sealed class UiShellSkeletonTests
{
    [Fact]
    public void Ui_Section_Can_Be_Mapped_To_Shell_Options()
    {
        var section = new UiSection(true, @"R:\runtime\config\ui");

        var result = UiOptionsBinder.Bind(section);

        Assert.True(result.IsSuccess);
        Assert.Equal("FusionCore", result.ShellOptions.ApplicationTitle);
        Assert.True(result.NavigationOptions.IncludeLogsEntry);
        Assert.True(result.StatusBarOptions.ShowHostState);
    }

    [Fact]
    public void Runtime_Summary_Models_Can_Express_Minimal_Semantics()
    {
        var summary = CreateRuntimeSummary();

        Assert.Equal("FusionCore Host", summary.Host.HostName);
        Assert.Equal("Started", summary.Host.HostState);
        Assert.Single(summary.Modules);
    }

    [Fact]
    public void Logs_Projection_Can_Express_Minimal_Semantics()
    {
        var projection = new LogsViewProjection(
            new[]
            {
                new LogEntrySummaryModel(
                    DateTimeOffset.Parse("2026-03-25T08:00:00+08:00"),
                    "Info",
                    "Runtime",
                    "宿主已完成最小启动。",
                    "FusionCore Host")
            },
            "当前显示 1 条日志摘要。");

        Assert.Single(projection.Entries);
        Assert.Contains("1 条", projection.SummaryText);
    }

    [Fact]
    public void Status_Bar_Model_Can_Express_Minimal_Semantics()
    {
        var model = new StatusBarModel(
            new[]
            {
                new StatusBarItem("页面", "运行"),
                new StatusBarItem("宿主", "Started")
            },
            new UiStatusMessage("当前仅显示只读摘要。"));

        Assert.Equal(2, model.Items.Count);
        Assert.Equal("当前仅显示只读摘要。", model.Message.Text);
    }

    [Fact]
    public void Ui_Composition_Root_Can_Create_Shell_With_Bootstrap_Context()
    {
        var bootstrapContext = CreateBootstrapContext();

        var shell = UiCompositionRoot.CreateShell(bootstrapContext);

        Assert.Equal("FusionCore", shell.ApplicationTitle);
        Assert.NotNull(shell.RuntimeSummary);
        Assert.NotNull(shell.LogsProjection);
        Assert.NotNull(shell.StatusBar);
    }

    [Fact]
    public void Runtime_And_Logs_ViewModels_Can_Receive_Summary_Models()
    {
        var runtimeSummary = CreateRuntimeSummary();
        var logsProjection = new LogsViewProjection(
            new[]
            {
                new LogEntrySummaryModel(
                    DateTimeOffset.Parse("2026-03-25T08:00:00+08:00"),
                    "Info",
                    "Runtime",
                    "宿主已完成最小启动。",
                    "FusionCore Host")
            },
            "当前显示 1 条日志摘要。");

        var runtimeViewModel = new RuntimeViewModel(runtimeSummary);
        var logsViewModel = new LogsViewModel(logsProjection);

        Assert.Equal("FusionCore Host", runtimeViewModel.Host.HostName);
        Assert.Single(runtimeViewModel.Modules);
        Assert.Single(logsViewModel.Entries);
    }

    [Fact]
    public void Runtime_Projection_Can_Be_Created_From_Host_Diagnostic_Info()
    {
        var diagnosticInfo = new HostDiagnosticInfo(
            new HostDescriptor("host-01", "FusionCore Host", "runtime-01", @"R:\runtime", HostRunMode.Simulation, "sim"),
            new RuntimeDescriptor(new RuntimeInstanceId("runtime-01"), @"R:\runtime", HostRunMode.Simulation, "sim"),
            HostState.Started,
            HostInitializationState.Initialized,
            Array.Empty<HostDependencyDescriptor>(),
            new ModuleCollectionSnapshot(
                new[] { new TestModuleDescriptor("ui", "FusionUI", "1.0.0") },
                new Dictionary<string, ModuleState> { ["ui"] = ModuleState.Started }));

        var summary = UiRuntimeProjection.FromDiagnostic(diagnosticInfo);

        Assert.Equal("FusionCore Host", summary.Host.HostName);
        Assert.Equal("runtime-01", summary.Host.RuntimeInstanceId);
        Assert.Single(summary.Modules);
    }

    [Fact]
    public void Default_Navigation_Skeleton_Can_Be_Generated()
    {
        var shell = UiCompositionRoot.CreateShell();

        Assert.True(shell.Navigation.Sections.Count >= 2);
        Assert.Contains(shell.Navigation.Sections.SelectMany(section => section.Items), item => item.Route == UiRoute.Logs);
    }

    private static RuntimeSummaryModel CreateRuntimeSummary()
    {
        return new RuntimeSummaryModel(
            new HostRuntimeSummaryModel("FusionCore Host", "Started", "Initialized", "runtime-01", "sim", @"R:\runtime"),
            new[]
            {
                new ModuleRuntimeSummaryModel("ui", "FusionUI", "Started")
            });
    }

    private static UiBootstrapContext CreateBootstrapContext()
    {
        var mappingResult = UiOptionsBinder.Bind(new UiSection(true, @"R:\runtime\config\ui"));
        var runtimeSummary = CreateRuntimeSummary();
        var logsProjection = new LogsViewProjection(
            new[]
            {
                new LogEntrySummaryModel(
                    DateTimeOffset.Parse("2026-03-25T08:00:00+08:00"),
                    "Info",
                    "Runtime",
                    "宿主已完成最小启动。",
                    "FusionCore Host")
            },
            "当前显示 1 条日志摘要。");

        return new UiBootstrapContext(
            mappingResult,
            runtimeSummary,
            logsProjection,
            new[]
            {
                new UiDependencyDescriptor("Config", true, "已注入最小配置映射。"),
                new UiDependencyDescriptor("Runtime", true, "已注入最小宿主摘要。"),
                new UiDependencyDescriptor("Logs", true, "已注入最小日志摘要。")
            });
    }

    private sealed record TestModuleDescriptor(
        string ModuleId,
        string ModuleName,
        string Version) : IFusionModuleDescriptor;
}
