using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionFA.Mappings;

/// <summary>
/// 表示领域作业到自动化作业语义的映射描述。
/// </summary>
public sealed record JobMapping(
    ControlJobId ControlJobId,
    ProcessJobId ProcessJobId,
    ControlState ControlState,
    string PublishedStateCode);
