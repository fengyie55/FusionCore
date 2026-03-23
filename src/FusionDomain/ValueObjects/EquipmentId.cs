using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识设备聚合根。
/// </summary>
/// <param name="Value">外部或内部使用的标识值。</param>
public sealed record EquipmentId(string Value) : ValueObjectBase;
