using FusionDomain.ValueObjects;
using FusionScheduler.Recovery;

namespace FusionScheduler.EventModels;

/// <summary>
/// 表示调度侧需要重新评估恢复意图的最小请求。
/// </summary>
public sealed record RecoveryEvaluationRequest(
    ProcessJobId? ProcessJobId,
    MaterialId? MaterialId,
    AlarmId? AlarmId,
    EquipmentId? EquipmentId,
    RecoveryReason? SuggestedReason);
