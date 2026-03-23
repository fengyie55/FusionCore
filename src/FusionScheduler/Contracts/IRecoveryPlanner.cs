using FusionScheduler.Models;
using FusionScheduler.Policies;
using FusionScheduler.Recovery;

namespace FusionScheduler.Contracts;

/// <summary>
/// 定义生成恢复计划的边界。
/// </summary>
public interface IRecoveryPlanner
{
    /// <summary>
    /// 为指定作业上下文和原因生成恢复计划骨架。
    /// </summary>
    RecoveryPlan CreateRecoveryPlan(
        ProductionJobContext jobContext,
        RecoveryReason reason,
        IRecoveryPolicy recoveryPolicy);
}
