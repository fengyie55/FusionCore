using FusionDomain.ValueObjects;
using FusionScheduler.Common;
using FusionScheduler.Models;

namespace FusionScheduler.Contracts;

/// <summary>
/// 提供对调度层协作数据的只读访问能力。
/// </summary>
public interface ISchedulerContextReader
{
    /// <summary>
    /// 获取指定控制作业的状态视图。
    /// </summary>
    JobStatusView GetJobStatus(ControlJobId controlJobId);

    /// <summary>
    /// 获取指定工艺作业的最新路径规划。
    /// </summary>
    RoutePlan? GetRoutePlan(ProcessJobId processJobId);

    /// <summary>
    /// 获取指定物料的最新上下文。
    /// </summary>
    MaterialContext? GetMaterialContext(MaterialId materialId);
}
