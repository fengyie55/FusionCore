using FusionDomain.Common;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Entities;

/// <summary>
/// 表示设备拓扑中的模块实体。
/// </summary>
public sealed class EquipmentModule : EntityBase<ModuleId>
{
    /// <summary>
    /// 初始化模块实体。
    /// </summary>
    public EquipmentModule(ModuleId id, string name, ModuleType moduleType)
        : base(id)
    {
        Name = name;
        ModuleType = moduleType;
    }

    /// <summary>
    /// 获取模块显示名称。
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 获取模块角色分类。
    /// </summary>
    public ModuleType ModuleType { get; }
}
