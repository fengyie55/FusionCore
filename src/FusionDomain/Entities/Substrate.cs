using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Entities;

/// <summary>
/// 表示在设备流转过程中被跟踪的基片实体。
/// </summary>
public sealed class Substrate : Material
{
    /// <summary>
    /// 初始化基片实体。
    /// </summary>
    public Substrate(SubstrateId substrateId, MaterialState state)
        : base(new MaterialId(substrateId.Value), state)
    {
        SubstrateId = substrateId;
    }

    /// <summary>
    /// 获取基片标识。
    /// </summary>
    public SubstrateId SubstrateId { get; }
}
