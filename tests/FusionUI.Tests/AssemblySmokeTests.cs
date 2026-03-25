namespace FusionUI.Tests;

public sealed class AssemblySmokeTests
{
    [Fact]
    public void FusionUI_Assembly_CanBeLoaded()
    {
        var assembly = typeof(FusionUI.Shell.MainWindow).Assembly;

        Assert.Equal("FusionUI", assembly.GetName().Name);
    }
}
