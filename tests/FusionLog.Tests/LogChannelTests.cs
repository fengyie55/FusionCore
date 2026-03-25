using FusionLog;

namespace FusionLog.Tests;

public sealed class LogChannelTests
{
    [Fact]
    public void Name_IsPreserved()
    {
        var channel = new LogChannel("system");

        Assert.Equal("system", channel.Name);
        Assert.Equal("system", channel.ToCategory().Name);
    }
}
