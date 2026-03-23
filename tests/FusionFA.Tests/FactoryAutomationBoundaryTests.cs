using FusionFA;
using FusionDomain.ValueObjects;

namespace FusionFA.Tests;

public sealed class FactoryAutomationBoundaryTests
{
    [Fact]
    public void Constructor_AssignsEquipmentId()
    {
        var equipmentId = new EquipmentId("EQ-FA");
        var boundary = new FactoryAutomationBoundary(equipmentId);

        Assert.Equal(equipmentId, boundary.EquipmentId);
    }
}
