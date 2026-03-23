using FusionKernel;

namespace FusionKernel.Tests;

public sealed class PlatformModuleTests
{
    [Fact]
    public void Name_IsStable()
    {
        Assert.Equal("FusionKernel", PlatformModule.Name);
    }
}
