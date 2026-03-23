namespace FusionDomain.Enums;

/// <summary>
/// 表示设备的粗粒度运行状态。
/// </summary>
public enum EquipmentState
{
    Unknown = 0,
    Offline = 1,
    Idle = 2,
    Running = 3,
    Paused = 4,
    Alarm = 5,
}
