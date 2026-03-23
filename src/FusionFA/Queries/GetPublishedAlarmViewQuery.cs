using FusionDomain.ValueObjects;

namespace FusionFA.Queries;

/// <summary>
/// 请求获取已发布的告警视图。
/// </summary>
public sealed record GetPublishedAlarmViewQuery(AlarmId AlarmId);
