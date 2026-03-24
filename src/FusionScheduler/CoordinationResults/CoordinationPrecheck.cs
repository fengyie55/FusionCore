namespace FusionScheduler.CoordinationResults;

/// <summary>
/// 表示执行前协调阶段需要确认的最小前置检查项。
/// </summary>
public sealed record CoordinationPrecheck(
    string CheckCode,
    bool IsSatisfied,
    string? Notes);
