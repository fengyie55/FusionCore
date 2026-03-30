using FusionStudio.Models;

namespace FusionStudio.ViewModels;

/// <summary>
/// 表示设备总览页的视图模型。
/// </summary>
public sealed class DeviceOverviewViewModel
{
    /// <summary>
    /// 获取设备总览摘要。
    /// </summary>
    public StudioDeviceOverviewModel Overview { get; }

    /// <summary>
    /// 初始化设备总览视图模型。
    /// </summary>
    public DeviceOverviewViewModel(StudioDeviceOverviewModel overview)
    {
        Overview = overview;
    }
}
