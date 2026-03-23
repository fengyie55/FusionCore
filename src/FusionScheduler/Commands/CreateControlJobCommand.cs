using FusionDomain.ValueObjects;

namespace FusionScheduler.Commands;

/// <summary>
/// 请求为控制作业与工艺作业对创建调度上下文。
/// </summary>
public sealed record CreateControlJobCommand(
    EquipmentId EquipmentId,
    ControlJobId ControlJobId,
    ProcessJobId ProcessJobId,
    RecipeId RecipeId);
