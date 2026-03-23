namespace FusionFA.States;

/// <summary>
/// 表示远程命令执行状态。
/// </summary>
public enum RemoteCommandExecutionState
{
    Unknown = 0,
    Accepted = 1,
    Rejected = 2,
    Completed = 3,
}
