using FusionDomain.Aggregates;
using FusionFA.Models;

namespace FusionFA.Contracts;

/// <summary>
/// 定义领域作业到自动化作业视图的映射边界。
/// </summary>
public interface IJobMapper
{
    /// <summary>
    /// 将控制作业与工艺作业映射为自动化作业视图。
    /// </summary>
    AutomationJobView Map(ControlJob controlJob, ProcessJob processJob);
}
