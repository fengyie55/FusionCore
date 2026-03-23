namespace FusionEquipment.Abstractions.Enums;

/// <summary>
/// 标识向编排层暴露的粗粒度模块能力。
/// </summary>
public enum ModuleCapability
{
    None = 0,
    Processing = 1,
    Transfer = 2,
    CarrierHandling = 3,
    Buffering = 4,
}
