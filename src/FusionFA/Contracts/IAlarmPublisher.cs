using FusionDomain.Entities;
using FusionFA.Models;

namespace FusionFA.Contracts;

/// <summary>
/// 定义告警对外发布边界。
/// </summary>
public interface IAlarmPublisher
{
    /// <summary>
    /// 将领域告警发布为自动化侧视图。
    /// </summary>
    AutomationAlarmView Publish(Alarm alarm);
}
