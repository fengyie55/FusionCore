using FusionStudio.Composition;
using FusionStudio.Navigation;
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
    }

    [Fact]
    public void Shell_Default_Page_Is_Device_Overview()
    {
        var shell = StudioCompositionRoot.CreateShell();

        Assert.Equal("FusionStudio", shell.ApplicationTitle);
        Assert.NotNull(shell.Navigation);
        Assert.NotNull(shell.Status);
        Assert.IsType<DeviceOverviewViewModel>(shell.CurrentViewModel);
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

        Assert.Equal("模块工作台", shell.CurrentViewTitle);
        Assert.IsType<ModuleWorkbenchViewModel>(shell.CurrentViewModel);
    }
}
