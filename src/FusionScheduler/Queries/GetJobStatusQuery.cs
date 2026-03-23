using FusionDomain.ValueObjects;

namespace FusionScheduler.Queries;

/// <summary>
/// 请求获取控制作业的状态视图。
/// </summary>
public sealed record GetJobStatusQuery(ControlJobId ControlJobId);
