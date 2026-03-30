using FusionKernel.Results;
using FusionStudio.Models;

namespace FusionStudio.Projections;

/// <summary>
/// 负责把宿主诊断信息投影为工作台可读摘要。
/// </summary>
public static class StudioRuntimeProjection
{
    /// <summary>
    /// 从宿主诊断信息创建运行态摘要。
    /// </summary>
    public static StudioRuntimeSummaryModel FromDiagnostic(HostDiagnosticInfo diagnosticInfo)
    {
        ArgumentNullException.ThrowIfNull(diagnosticInfo);

        var modules = diagnosticInfo.Modules.Modules
            .Select(descriptor =>
            {
                diagnosticInfo.Modules.States.TryGetValue(descriptor.ModuleId, out var state);
                return new StudioModuleSummaryModel(
                    descriptor.ModuleId,
                    descriptor.ModuleName,
                    state.ToString());
            })
            .ToArray();

        return new StudioRuntimeSummaryModel(
            diagnosticInfo.Host.HostName,
            diagnosticInfo.State.ToString(),
            diagnosticInfo.InitializationState.ToString(),
            diagnosticInfo.Runtime.InstanceId.Value,
            diagnosticInfo.Runtime.Profile ?? "n/a",
            diagnosticInfo.Runtime.RuntimeRoot,
            modules);
    }
}
