using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识基片。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record SubstrateId(string Value) : ValueObjectBase;
