using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识载具。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record CarrierId(string Value) : ValueObjectBase;
