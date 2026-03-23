using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识设备模块实体。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record ModuleId(string Value) : ValueObjectBase;
