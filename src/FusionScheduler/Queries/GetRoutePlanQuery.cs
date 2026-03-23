using FusionDomain.ValueObjects;

namespace FusionScheduler.Queries;

/// <summary>
/// 请求获取工艺作业的最新路径规划。
/// </summary>
public sealed record GetRoutePlanQuery(ProcessJobId ProcessJobId);
