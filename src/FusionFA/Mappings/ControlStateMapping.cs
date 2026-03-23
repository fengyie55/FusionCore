using FusionDomain.Enums;

namespace FusionFA.Mappings;

/// <summary>
/// 表示领域控制状态到自动化状态代码的映射描述。
/// </summary>
public sealed record ControlStateMapping(
    ControlState DomainState,
    string AutomationStateCode);
