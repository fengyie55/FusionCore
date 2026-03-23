using FusionDomain.ValueObjects;

namespace FusionFA;

public sealed class FactoryAutomationBoundary
{
    public EquipmentId EquipmentId { get; }

    public FactoryAutomationBoundary(EquipmentId equipmentId)
    {
        EquipmentId = equipmentId;
    }
}
