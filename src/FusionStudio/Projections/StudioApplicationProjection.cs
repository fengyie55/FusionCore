using FusionApp.Composition;
using FusionLog.Entries;
using FusionStudio.Composition;

namespace FusionStudio.Projections;

/// <summary>
/// 负责把应用装配结果投影为 FusionStudio 启动上下文。
/// </summary>
public static class StudioApplicationProjection
{
    /// <summary>
    /// 从应用装配结果创建工作台启动上下文。
    /// </summary>
    public static StudioBootstrapContext CreateBootstrapContext(
        ApplicationAssembly assembly,
        IReadOnlyCollection<LogEntry>? entries = null)
    {
        return StudioCompositionRoot.CreateBootstrapContext(assembly, entries);
    }
}
