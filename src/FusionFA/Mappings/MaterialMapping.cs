using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionFA.Mappings;

/// <summary>
/// 表示领域物料到自动化物料视图的映射描述。
/// </summary>
public sealed record MaterialMapping(
    MaterialId MaterialId,
    MaterialState MaterialState,
    string MaterialType,
    string StateCode);
