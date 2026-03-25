using FusionKernel;

namespace FusionKernel.Tests;

public sealed class PlatformModuleTests
{
    [Fact]
    public void Platform_Module_Exposes_Expected_Identity()
    {
        var module = new PlatformModule();

        Assert.Equal("FusionKernel.PlatformModule", module.Id);
        Assert.Equal(nameof(PlatformModule), module.Name);
        Assert.Equal("FusionKernel.PlatformModule", module.Descriptor.ModuleId);
    }
}
