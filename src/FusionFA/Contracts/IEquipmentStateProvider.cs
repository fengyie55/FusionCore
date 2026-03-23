using FusionDomain.Aggregates;
using FusionDomain.Enums;
using FusionFA.Models;

namespace FusionFA.Contracts;

/// <summary>
/// 定义设备状态到自动化快照的读取边界。
/// </summary>
public interface IEquipmentStateProvider
{
    /// <summary>
    /// 为指定设备生成自动化快照。
    /// </summary>
    EquipmentAutomationSnapshot CreateSnapshot(Equipment equipment, ControlState controlState);
}
