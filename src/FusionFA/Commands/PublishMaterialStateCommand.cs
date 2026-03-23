using FusionDomain.Entities;

namespace FusionFA.Commands;

/// <summary>
/// 请求发布物料状态到自动化侧。
/// </summary>
public sealed record PublishMaterialStateCommand(Material Material);
