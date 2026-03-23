using FusionDomain.ValueObjects;
using FusionEquipment.Abstractions.Contracts;

namespace FusionScheduler.Models;

/// <summary>
/// 表示单个调度派发任务骨架。
/// </summary>
public sealed record DispatchTask(
    string TaskId,
    MaterialId MaterialId,
    ModuleId? SourceModuleId,
    ModuleId? TargetModuleId,
    IEquipmentModule? TargetModule,
    string ActionName);
