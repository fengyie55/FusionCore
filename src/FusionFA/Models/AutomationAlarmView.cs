using FusionDomain.ValueObjects;

namespace FusionFA.Models;

/// <summary>
/// 表示面向自动化侧发布的告警视图。
/// </summary>
public sealed record AutomationAlarmView(
    AlarmId AlarmId,
    string AlarmCode,
    string SeverityCode,
    string Description);
