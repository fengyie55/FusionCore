using FusionDomain.ValueObjects;

namespace FusionApp;

public sealed class ApplicationBoundary
{
    public EquipmentId EquipmentId { get; }

    public ApplicationBoundary(EquipmentId equipmentId)
    {
        EquipmentId = equipmentId;
    }
}
