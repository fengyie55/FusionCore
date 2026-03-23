using FusionDomain.Common;
using FusionDomain.ValueObjects;

namespace FusionDomain.Entities;

/// <summary>
/// 表示设备中的逻辑或物理工位。
/// </summary>
public sealed class Station : EntityBase<StationId>
{
    /// <summary>
    /// 初始化工位实体。
    /// </summary>
    public Station(StationId id, string name)
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// 获取工位名称。
    /// </summary>
    public string Name { get; }
}
