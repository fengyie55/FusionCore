namespace FusionDomain.Enums;

/// <summary>
/// 表示受跟踪物料的最小生命周期状态。
/// </summary>
public enum MaterialState
{
    Unknown = 0,
    Registered = 1,
    Waiting = 2,
    InProcess = 3,
    Completed = 4,
    Hold = 5,
}
