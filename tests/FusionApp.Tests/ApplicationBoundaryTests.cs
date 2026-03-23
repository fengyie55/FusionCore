using FusionApp;
using FusionDomain.ValueObjects;

namespace FusionApp.Tests;

public sealed class ApplicationBoundaryTests
{
    [Fact]
    public void Constructor_AssignsEquipmentId()
    {
        var equipmentId = new EquipmentId("EQ-APP");
        var boundary = new ApplicationBoundary(equipmentId);

        Assert.Equal(equipmentId, boundary.EquipmentId);
    }
}
