using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识受跟踪的物料。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record MaterialId(string Value) : ValueObjectBase;
