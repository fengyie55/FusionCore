using FusionScheduler.Models;
using FusionScheduler.Policies;

namespace FusionScheduler.Contracts;

/// <summary>
/// 定义将作业上下文转换为路径规划结果的边界。
/// </summary>
public interface IRoutePlanner
{
    /// <summary>
    /// 为指定生产作业上下文生成路径规划骨架。
    /// </summary>
    RoutePlan PlanRoute(ProductionJobContext jobContext, IRoutingPolicy routingPolicy);
}
