using FusionDomain.Entities;
using FusionDomain.ValueObjects;
using FusionEquipment.Abstractions.Contracts;

namespace FusionScheduler.Models;

/// <summary>
/// 表示当前面向调度层的物料视图。
/// </summary>
public sealed record MaterialContext(
    MaterialId MaterialId,
    Material Material,
    ModuleId? CurrentModuleId,
    string? CurrentLocation,
    IEquipmentModule? ReservedModule);
