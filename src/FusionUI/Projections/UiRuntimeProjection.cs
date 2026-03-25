using FusionKernel.Results;
using FusionUI.Models;

namespace FusionUI.Projections;

/// <summary>
/// 负责将宿主运行态摘要投影为 UI 可读模型。
/// </summary>
public static class UiRuntimeProjection
{
    /// <summary>
    /// 从宿主诊断信息构建运行态摘要。
    /// </summary>
    public static RuntimeSummaryModel FromDiagnostic(HostDiagnosticInfo diagnosticInfo)
    {
        var host = new HostRuntimeSummaryModel(
            diagnosticInfo.Host.HostName,
            diagnosticInfo.State.ToString(),
            diagnosticInfo.InitializationState.ToString(),
            diagnosticInfo.Runtime.InstanceId.Value,
            diagnosticInfo.Runtime.Profile,
            diagnosticInfo.Runtime.RuntimeRoot);

        var modules = diagnosticInfo.Modules.Modules
            .Select(module =>
            {
                var state = diagnosticInfo.Modules.States.TryGetValue(module.ModuleId, out var moduleState)
                    ? moduleState.ToString()
                    : "Unknown";

                return new ModuleRuntimeSummaryModel(module.ModuleId, module.ModuleName, state);
            })
            .ToList();

        return new RuntimeSummaryModel(host, modules);
    }
}
