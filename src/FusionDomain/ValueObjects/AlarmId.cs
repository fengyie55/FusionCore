using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识告警定义或告警实例。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record AlarmId(string Value) : ValueObjectBase;
