using FusionDomain.ValueObjects;

namespace FusionFA.Models;

/// <summary>
/// 表示面向自动化侧发布的物料视图。
/// </summary>
public sealed record AutomationMaterialView(
    MaterialId MaterialId,
    string MaterialType,
    string MaterialStateCode,
    string? LocationCode);
