using FusionEquipment.Abstractions.Context;
using FusionEquipment.Abstractions.Enums;
using FusionEquipment.Abstractions.Lifecycle;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionEquipment.Abstractions.Contracts;

/// <summary>
/// 定义所有设备模块共享的最小契约。
/// </summary>
public interface IEquipmentModule : IModuleLifecycle
{
    /// <summary>
    /// 获取所属设备标识。
    /// </summary>
    EquipmentId EquipmentId { get; }

    /// <summary>
    /// 获取模块标识。
    /// </summary>
    ModuleId ModuleId { get; }

    /// <summary>
    /// 获取模块名称。
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 获取模块类型分类。
    /// </summary>
    ModuleType ModuleType { get; }

    /// <summary>
    /// 获取模块的高层运行上下文。
    /// </summary>
    ModuleContext Context { get; }

    /// <summary>
    /// 获取模块对外发布的能力集合。
    /// </summary>
    IReadOnlyCollection<ModuleCapability> Capabilities { get; }
}
