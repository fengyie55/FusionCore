using FusionDomain.ValueObjects;

namespace FusionDomain.Events.Job;

/// <summary>
/// 表示工艺作业已创建。
/// </summary>
public sealed record ProcessJobCreatedEvent(
    ProcessJobId ProcessJobId,
    RecipeId RecipeId) : DomainEvent;
