using FusionFA.Commands;
using FusionFA.Models;

namespace FusionFA.Contracts;

/// <summary>
/// 定义面向自动化出口的统一服务入口。
/// </summary>
public interface IFAService
{
    /// <summary>
    /// 生成当前设备的自动化快照。
    /// </summary>
    EquipmentAutomationSnapshot GetCurrentSnapshot();

    /// <summary>
    /// 发布设备状态视图。
    /// </summary>
    EquipmentAutomationSnapshot PublishEquipmentState(PublishEquipmentStateCommand command);

    /// <summary>
    /// 发布告警视图。
    /// </summary>
    AutomationAlarmView PublishAlarm(PublishAlarmCommand command);

    /// <summary>
    /// 发布作业视图。
    /// </summary>
    AutomationJobView PublishJobState(PublishJobStateCommand command);

    /// <summary>
    /// 发布物料视图。
    /// </summary>
    AutomationMaterialView PublishMaterialState(PublishMaterialStateCommand command);

    /// <summary>
    /// 执行远程命令请求。
    /// </summary>
    RemoteCommandResult ExecuteRemoteCommand(ExecuteRemoteCommandRequest command);
}
