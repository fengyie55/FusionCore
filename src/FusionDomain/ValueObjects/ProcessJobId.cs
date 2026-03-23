using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识工艺作业聚合根。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record ProcessJobId(string Value) : ValueObjectBase;
