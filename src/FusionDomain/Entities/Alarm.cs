using FusionDomain.Common;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Entities;

/// <summary>
/// 表示对领域层与自动化流程可见的告警实体。
/// </summary>
public sealed class Alarm : EntityBase<AlarmId>
{
    /// <summary>
    /// 初始化告警实体。
    /// </summary>
    public Alarm(AlarmId id, string code, AlarmSeverity severity)
        : base(id)
    {
        Code = code;
        Severity = severity;
    }

    /// <summary>
    /// 获取告警代码。
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// 获取告警严重级别。
    /// </summary>
    public AlarmSeverity Severity { get; }
}
