using FusionDomain.Common;
using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionDomain.Entities;

/// <summary>
/// 表示领域模型中的受跟踪物料。
/// </summary>
public class Material : EntityBase<MaterialId>
{
    /// <summary>
    /// 初始化物料实体。
    /// </summary>
    public Material(MaterialId id, MaterialState state)
        : base(id)
    {
        State = state;
    }

    /// <summary>
    /// 获取当前物料状态。
    /// </summary>
    public MaterialState State { get; }
}
