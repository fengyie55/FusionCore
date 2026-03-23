using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识控制作业聚合根。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record ControlJobId(string Value) : ValueObjectBase;
