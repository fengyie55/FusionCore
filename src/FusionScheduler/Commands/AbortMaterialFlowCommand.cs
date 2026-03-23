using FusionDomain.ValueObjects;
using FusionScheduler.Recovery;

namespace FusionScheduler.Commands;

/// <summary>
/// 请求对物料流转执行中止处理。
/// </summary>
public sealed record AbortMaterialFlowCommand(
    ProcessJobId ProcessJobId,
    MaterialId MaterialId,
    RecoveryReason Reason);
