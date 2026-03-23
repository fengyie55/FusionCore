using FusionScheduler.Commands;
using FusionScheduler.Common;
using FusionScheduler.Models;

namespace FusionScheduler.Contracts;

/// <summary>
/// 定义面向应用层的调度协作入口。
/// </summary>
public interface ISchedulerService
{
    /// <summary>
    /// 为控制作业请求创建生产作业上下文。
    /// </summary>
    ProductionJobContext CreateControlJob(CreateControlJobCommand command);

    /// <summary>
    /// 为已有生产作业上下文启动调度。
    /// </summary>
    RoutePlan StartScheduling(StartSchedulingCommand command);

    /// <summary>
    /// 为已有作业上下文请求中止物料流转。
    /// </summary>
    RecoveryPlan AbortMaterialFlow(AbortMaterialFlowCommand command);

    /// <summary>
    /// 为作业上下文关联的物料请求卸载规划。
    /// </summary>
    RecoveryPlan RequestMaterialUnload(RequestMaterialUnloadCommand command);

    /// <summary>
    /// 获取指定作业的轻量状态视图。
    /// </summary>
    JobStatusView GetJobStatus(ProductionJobContext jobContext);
}
