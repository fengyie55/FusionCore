using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Common;

/// <summary>
/// 表示事件负载中使用的最小位置引用。
/// </summary>
public sealed record LocationReference(
    ModuleId? ModuleId,
    string? LocationCode);
