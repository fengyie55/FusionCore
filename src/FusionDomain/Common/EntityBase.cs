namespace FusionDomain.Common;

/// <summary>
/// 为领域实体提供最小基类。
/// </summary>
/// <typeparam name="TId">实体标识类型。</typeparam>
public abstract class EntityBase<TId>
    where TId : notnull
{
    /// <summary>
    /// 使用指定标识初始化实体。
    /// </summary>
    protected EntityBase(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// 获取实体的稳定标识。
    /// </summary>
    public TId Id { get; }
}
