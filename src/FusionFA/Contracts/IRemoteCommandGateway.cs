using FusionFA.Models;

namespace FusionFA.Contracts;

/// <summary>
/// 定义远程命令受控入口边界。
/// </summary>
public interface IRemoteCommandGateway
{
    /// <summary>
    /// 执行远程命令请求并返回结果。
    /// </summary>
    RemoteCommandResult Execute(RemoteCommandRequest request);
}
