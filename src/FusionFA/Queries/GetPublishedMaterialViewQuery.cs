using FusionDomain.ValueObjects;

namespace FusionFA.Queries;

/// <summary>
/// 请求获取已发布的物料视图。
/// </summary>
public sealed record GetPublishedMaterialViewQuery(MaterialId MaterialId);
