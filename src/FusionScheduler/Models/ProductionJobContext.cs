using FusionDomain.Aggregates;
using FusionDomain.ValueObjects;

namespace FusionScheduler.Models;

/// <summary>
/// 表示面向调度层的最小生产作业上下文。
/// </summary>
public sealed record ProductionJobContext(
    EquipmentId EquipmentId,
    ControlJob ControlJob,
    ProcessJob ProcessJob,
    RecipeId RecipeId);
