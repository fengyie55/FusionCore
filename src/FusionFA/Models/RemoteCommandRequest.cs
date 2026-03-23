using FusionDomain.ValueObjects;
using FusionFA.Common;

namespace FusionFA.Models;

/// <summary>
/// 表示来自自动化侧的远程命令请求。
/// </summary>
public sealed record RemoteCommandRequest(
    string CommandName,
    AutomationObjectType TargetType,
    string? TargetKey,
    EquipmentId EquipmentId,
    IReadOnlyDictionary<string, string> Arguments);
