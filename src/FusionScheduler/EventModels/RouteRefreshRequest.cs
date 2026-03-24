using FusionDomain.ValueObjects;

namespace FusionScheduler.EventModels;

/// <summary>
/// 表示调度侧需要重新评估路径或路由上下文的最小请求。
/// </summary>
public sealed record RouteRefreshRequest(
    ProcessJobId? ProcessJobId,
    MaterialId? MaterialId,
    EquipmentId? EquipmentId,
    string ReasonCode);
