using FusionDomain.ValueObjects;

namespace FusionFA.Queries;

/// <summary>
/// 请求获取已发布的作业视图。
/// </summary>
public sealed record GetPublishedJobViewQuery(ControlJobId ControlJobId, ProcessJobId ProcessJobId);
