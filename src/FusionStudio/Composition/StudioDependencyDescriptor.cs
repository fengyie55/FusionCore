namespace FusionStudio.Composition;

/// <summary>
/// 表示 FusionStudio 与外部平台边界的最小依赖摘要。
/// </summary>
public sealed record StudioDependencyDescriptor(
    string DependencyName,
    bool IsConnected,
    string Summary);
