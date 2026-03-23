using FusionDomain.Common;

namespace FusionDomain.ValueObjects;

/// <summary>
/// 标识配方定义。
/// </summary>
/// <param name="Value">标识值。</param>
public sealed record RecipeId(string Value) : ValueObjectBase;
