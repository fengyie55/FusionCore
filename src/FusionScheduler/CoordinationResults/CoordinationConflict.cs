namespace FusionScheduler.CoordinationResults;

/// <summary>
/// 表示执行前协调阶段发现的最小冲突信息。
/// </summary>
public sealed record CoordinationConflict(
    string ConflictCode,
    string? RelatedResource,
    string? Description);
