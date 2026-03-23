using FusionDomain.ValueObjects;

namespace FusionScheduler.Models;

/// <summary>
/// 表示工艺作业的最小路径规划结果。
/// </summary>
public sealed record RoutePlan(
    ProcessJobId ProcessJobId,
    EquipmentId EquipmentId,
    IReadOnlyList<DispatchTask> Tasks);
