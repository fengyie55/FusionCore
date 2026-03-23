using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionFA.Mappings;

/// <summary>
/// 表示领域告警到自动化告警语义的映射描述。
/// </summary>
public sealed record AlarmMapping(
    AlarmId AlarmId,
    AlarmSeverity Severity,
    string AlarmCode,
    string SeverityCode);
