using FusionDomain.ValueObjects;

namespace FusionScheduler.EventModels;

/// <summary>
/// 表示调度侧需要刷新作业上下文的最小请求。
/// </summary>
public sealed record JobContextRefreshRequest(
    ControlJobId? ControlJobId,
    ProcessJobId? ProcessJobId,
    RecipeId? RecipeId,
    string ReasonCode);
