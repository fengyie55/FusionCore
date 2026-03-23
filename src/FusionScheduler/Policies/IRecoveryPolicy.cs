using FusionScheduler.Models;
using FusionScheduler.Recovery;

namespace FusionScheduler.Policies;

/// <summary>
/// 定义恢复规划所需的最小策略元数据。
/// </summary>
public interface IRecoveryPolicy
{
    /// <summary>
    /// 获取策略名称。
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 判断策略是否适用于指定作业上下文和恢复原因。
    /// </summary>
    bool AppliesTo(ProductionJobContext jobContext, RecoveryReason reason);
}
