using FusionFA.Models;

namespace FusionFA.Commands;

/// <summary>
/// 请求执行自动化侧远程命令。
/// </summary>
public sealed record ExecuteRemoteCommandRequest(RemoteCommandRequest Request);
