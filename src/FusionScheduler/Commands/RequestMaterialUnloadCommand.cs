using FusionDomain.ValueObjects;
using FusionScheduler.Recovery;

namespace FusionScheduler.Commands;

/// <summary>
/// 请求对物料执行卸载规划。
/// </summary>
public sealed record RequestMaterialUnloadCommand(
    ProcessJobId ProcessJobId,
    MaterialId MaterialId,
    RecoveryReason Reason);
