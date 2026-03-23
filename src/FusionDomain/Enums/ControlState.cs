namespace FusionDomain.Enums;

/// <summary>
/// 表示作业或设备的最小控制状态。
/// </summary>
public enum ControlState
{
    Unknown = 0,
    Created = 1,
    Queued = 2,
    Active = 3,
    Completed = 4,
    Aborted = 5,
}
