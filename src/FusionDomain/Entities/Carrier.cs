using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Entities;

/// <summary>
/// 表示承载并运输物料的载具实体。
/// </summary>
public sealed class Carrier : Material
{
    /// <summary>
    /// 初始化载具实体。
    /// </summary>
    public Carrier(CarrierId carrierId, MaterialState state)
        : base(new MaterialId(carrierId.Value), state)
    {
        CarrierId = carrierId;
    }

    /// <summary>
    /// 获取载具标识。
    /// </summary>
    public CarrierId CarrierId { get; }
}
