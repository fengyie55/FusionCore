using FusionDomain.ValueObjects;

namespace FusionFA.Queries;

/// <summary>
/// 请求获取当前设备自动化快照。
/// </summary>
public sealed record GetCurrentEquipmentAutomationSnapshotQuery(EquipmentId EquipmentId);
