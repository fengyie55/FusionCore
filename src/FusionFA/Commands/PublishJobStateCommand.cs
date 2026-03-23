using FusionDomain.Aggregates;

namespace FusionFA.Commands;

/// <summary>
/// 请求发布作业状态到自动化侧。
/// </summary>
public sealed record PublishJobStateCommand(
    ControlJob ControlJob,
    ProcessJob ProcessJob);
