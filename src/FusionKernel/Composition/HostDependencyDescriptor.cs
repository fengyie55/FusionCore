namespace FusionKernel.Composition;

/// <summary>
/// 描述宿主接入的外部依赖边界。
/// </summary>
public sealed record HostDependencyDescriptor(
    string Name,
    string? TypeName,
    bool IsConfigured);
