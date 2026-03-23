using FusionScheduler.Models;

namespace FusionScheduler.Policies;

/// <summary>
/// 定义路径规划决策所需的最小策略元数据。
/// </summary>
public interface IRoutingPolicy
{
    /// <summary>
    /// 获取策略名称。
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 判断策略是否适用于指定作业上下文。
    /// </summary>
    bool AppliesTo(ProductionJobContext jobContext);
}
