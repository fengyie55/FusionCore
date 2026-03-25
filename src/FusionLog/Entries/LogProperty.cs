namespace FusionLog.Entries;

/// <summary>
/// 表示日志附加字段。
/// </summary>
/// <param name="Name">字段名称。</param>
/// <param name="Value">字段值。</param>
public sealed record LogProperty(
    string Name,
    string? Value);
