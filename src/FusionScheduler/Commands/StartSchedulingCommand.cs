using FusionDomain.ValueObjects;

namespace FusionScheduler.Commands;

/// <summary>
/// 请求为已有工艺作业创建路径规划。
/// </summary>
public sealed record StartSchedulingCommand(
    EquipmentId EquipmentId,
    ProcessJobId ProcessJobId);
