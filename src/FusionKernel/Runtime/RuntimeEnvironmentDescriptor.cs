namespace FusionKernel.Runtime;

/// <summary>
/// 描述运行环境的最小信息。
/// </summary>
public sealed record RuntimeEnvironmentDescriptor(
    string? Profile,
    string RuntimeRoot);
