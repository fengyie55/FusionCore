using FusionDomain.Common;
using FusionDomain.Entities;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Aggregates;

/// <summary>
/// 表示设备聚合根。
/// </summary>
public sealed class Equipment : EntityBase<EquipmentId>
{
    /// <summary>
    /// 初始化设备聚合根。
    /// </summary>
    public Equipment(EquipmentId id, string name, EquipmentState state, IReadOnlyCollection<EquipmentModule> modules)
        : base(id)
    {
        Name = name;
        State = state;
        Modules = modules;
    }

    /// <summary>
    /// 获取设备名称。
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 获取当前设备状态。
    /// </summary>
    public EquipmentState State { get; }

    /// <summary>
    /// 获取属于该设备聚合的模块集合。
    /// </summary>
    public IReadOnlyCollection<EquipmentModule> Modules { get; }
}
