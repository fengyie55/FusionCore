using FusionDomain.Aggregates;
using FusionDomain.Enums;

namespace FusionFA.Commands;

/// <summary>
/// 请求发布设备状态到自动化侧。
/// </summary>
public sealed record PublishEquipmentStateCommand(
    Equipment Equipment,
    ControlState ControlState);
