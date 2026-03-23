using FusionFA.States;

namespace FusionFA.Models;

/// <summary>
/// 表示远程命令执行结果视图。
/// </summary>
public sealed record RemoteCommandResult(
    string CommandName,
    RemoteCommandExecutionState State,
    string Message);
