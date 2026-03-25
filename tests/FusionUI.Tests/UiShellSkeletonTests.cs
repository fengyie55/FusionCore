using FusionUI.Composition;
using FusionUI.Navigation;
using FusionUI.ViewModels;

namespace FusionUI.Tests;

public sealed class UiShellSkeletonTests
{
    [Fact]
    public void Shell_ViewModel_Can_Be_Created()
    {
        var shell = UiCompositionRoot.CreateShell();

        Assert.Equal("FusionCore", shell.ApplicationTitle);
        Assert.NotNull(shell.Navigation);
        Assert.NotNull(shell.CurrentViewModel);
    }

    [Fact]
    public void Navigation_Item_And_Section_Can_Express_Minimal_Semantics()
    {
        var item = new NavigationItem(UiRoute.Overview, "概览", "概览入口。", "主视图");
        var section = new NavigationSection("主视图", new[] { item });

        Assert.Equal(UiRoute.Overview, item.Route);
        Assert.Single(section.Items);
        Assert.Equal("主视图", section.Title);
    }

    [Fact]
    public void Default_Navigation_Skeleton_Can_Be_Generated()
    {
        var shell = UiCompositionRoot.CreateShell();

        Assert.True(shell.Navigation.Sections.Count >= 2);
        Assert.Contains(shell.Navigation.Sections.SelectMany(section => section.Items), item => item.Route == UiRoute.Logs);
    }

    [Fact]
    public void Placeholder_ViewModels_Can_Be_Instantiated()
    {
        PlaceholderViewModelBase[] viewModels =
        {
            new OverviewViewModel(),
            new OperatorViewModel(),
            new EngineerViewModel(),
            new RuntimeViewModel(),
            new LogsViewModel(),
            new EquipmentViewModel()
        };

        Assert.All(viewModels, viewModel => Assert.False(string.IsNullOrWhiteSpace(viewModel.Title)));
    }

    [Fact]
    public void Ui_Composition_Root_Can_Create_Runtime_Descriptor()
    {
        var descriptor = UiCompositionRoot.CreateRuntimeDescriptor();

        Assert.Equal("FusionCore", descriptor.ApplicationTitle);
        Assert.Contains("主视图", descriptor.NavigationSections);
    }
}
