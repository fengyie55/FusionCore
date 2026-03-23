using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识设备结构中的工位。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record StationId(string Value) : ValueObjectBase;
