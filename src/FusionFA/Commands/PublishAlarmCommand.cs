using FusionDomain.Entities;

namespace FusionFA.Commands;

/// <summary>
/// 请求发布告警到自动化侧。
/// </summary>
public sealed record PublishAlarmCommand(Alarm Alarm);
