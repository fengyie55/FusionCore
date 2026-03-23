using FusionConfig;

namespace FusionConfig.Tests;

public sealed class ConfigurationSectionTests
{
    [Fact]
    public void Name_IsPreserved()
    {
        var section = new ConfigurationSection("runtime");

        Assert.Equal("runtime", section.Name);
    }
}
