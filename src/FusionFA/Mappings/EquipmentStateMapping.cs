using FusionDomain.Enums;

namespace FusionFA.Mappings;

/// <summary>
/// 表示领域设备状态到自动化状态代码的映射描述。
/// </summary>
public sealed record EquipmentStateMapping(
    EquipmentState DomainState,
    string AutomationStateCode);
