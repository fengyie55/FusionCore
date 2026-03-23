namespace FusionScheduler.Recovery;

/// <summary>
/// 表示生成恢复计划的最小原因集合。
/// </summary>
public enum RecoveryReason
{
    Unknown = 0,
    OperatorAbort = 1,
    ModuleUnavailable = 2,
    Interlock = 3,
    MaterialUnloadRequested = 4,
}
