using FusionStudio.Composition;
using FusionStudio.Navigation;
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
        Assert.Equal(4, context.RuntimeDescriptor.Dependencies.Count);
    }

    [Fact]
    public void Shell_Can_Be_Created_From_Default_Context()
    {
        var shell = StudioCompositionRoot.CreateShell();

        Assert.Equal("FusionStudio", shell.ApplicationTitle);
        Assert.NotNull(shell.Navigation);
        Assert.NotNull(shell.Status);
        Assert.NotNull(shell.CurrentViewModel);
    }

    [Fact]
    public void Navigation_Can_Switch_To_Debug_Assistant()
    {
        var shell = StudioCompositionRoot.CreateShell();
        var debugItem = shell.Navigation.Sections
            .SelectMany(section => section.Items)
            .Single(item => item.Route == StudioRoute.DebugAssistant);

        shell.NavigateTo(debugItem);

        Assert.Equal("调试助手", shell.CurrentViewTitle);
        Assert.IsType<DebugAssistantViewModel>(shell.CurrentViewModel);
    }
}
