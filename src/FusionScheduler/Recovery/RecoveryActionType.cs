namespace FusionScheduler.Recovery;

/// <summary>
/// 表示调度层考虑的粗粒度恢复动作。
/// </summary>
public enum RecoveryActionType
{
    None = 0,
    Hold = 1,
    Return = 2,
    Unload = 3,
    Retry = 4,
}
