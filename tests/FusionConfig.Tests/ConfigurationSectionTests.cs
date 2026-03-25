using FusionConfig;
using FusionConfig.Abstractions;
using FusionConfig.Sections;

namespace FusionConfig.Tests;

public sealed class ConfigurationSectionTests
{
    [Fact]
    public void Section_Name_And_Key_Are_Preserved()
    {
        IConfigurationSection section = new ConfigurationSection("runtime");

        Assert.Equal("runtime", section.SectionName);
        Assert.Equal("runtime", section.SectionKey.Value);
    }

    [Fact]
    public void Stable_Section_Key_Can_Express_Core_Section_Identity()
    {
        var key = ConfigurationSectionKeys.Logging;

        Assert.Equal("Logging", key.Value);
        Assert.Equal(ConfigurationSectionKey.From("Logging"), key);
    }
}
