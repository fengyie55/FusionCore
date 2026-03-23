using FusionDomain.ValueObjects;

namespace FusionEquipment.Abstractions.Context;

/// <summary>
/// 承载设备模块的最小识别上下文。
/// </summary>
public sealed class ModuleContext
{
    /// <summary>
    /// 初始化模块上下文。
    /// </summary>
    public ModuleContext(EquipmentId equipmentId, ModuleId moduleId, string moduleName)
    {
        EquipmentId = equipmentId;
        ModuleId = moduleId;
        ModuleName = moduleName;
    }

    /// <summary>
    /// 获取拥有该模块的设备标识。
    /// </summary>
    public EquipmentId EquipmentId { get; }

    /// <summary>
    /// 获取模块标识。
    /// </summary>
    public ModuleId ModuleId { get; }

    /// <summary>
    /// 获取模块显示名称。
    /// </summary>
    public string ModuleName { get; }
}
