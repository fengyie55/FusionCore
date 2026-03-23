using FusionDomain.ValueObjects;

namespace FusionScheduler.Queries;

/// <summary>
/// 请求获取受跟踪物料的当前位置视图。
/// </summary>
public sealed record GetMaterialLocationQuery(MaterialId MaterialId);
