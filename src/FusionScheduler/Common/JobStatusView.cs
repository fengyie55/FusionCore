using FusionDomain.Enums;
using FusionDomain.ValueObjects;

namespace FusionScheduler.Common;

/// <summary>
/// 表示控制作业与工艺作业对的轻量调度状态视图。
/// </summary>
public sealed record JobStatusView(
    ControlJobId ControlJobId,
    ProcessJobId ProcessJobId,
    ControlState State,
    string StatusText);
