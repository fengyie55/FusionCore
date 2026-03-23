using FusionDomain.Common;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Aggregates;

/// <summary>
/// 表示与工厂侧语义对齐的控制作业聚合根。
/// </summary>
public sealed class ControlJob : EntityBase<ControlJobId>
{
    /// <summary>
    /// 初始化控制作业聚合根。
    /// </summary>
    public ControlJob(ControlJobId id, ControlState state)
        : base(id)
    {
        State = state;
    }

    /// <summary>
    /// 获取当前控制作业状态。
    /// </summary>
    public ControlState State { get; }
}
