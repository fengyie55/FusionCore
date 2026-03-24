using FusionDomain.ValueObjects;

namespace FusionScheduler.EventModels;

/// <summary>
/// 表示调度侧需要刷新物料或基片跟踪上下文的最小请求。
/// </summary>
public sealed record TrackingRefreshRequest(
    MaterialId? MaterialId,
    CarrierId? CarrierId,
    SubstrateId? SubstrateId,
    ModuleId? ModuleId,
    string? LocationCode);
