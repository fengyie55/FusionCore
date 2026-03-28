namespace FusionStudio.Composition;

/// <summary>
/// 表示 FusionStudio 启动后的最小运行摘要。
/// </summary>
public sealed record StudioRuntimeDescriptor(
    string Title,
    string CurrentProfile,
    string RuntimeRootSummary,
    IReadOnlyCollection<StudioDependencyDescriptor> Dependencies);
