using FusionDomain.Events.Recipe;
using FusionScheduler.EventContracts;

namespace FusionScheduler.EventConsumers;

/// <summary>
/// 定义调度侧关心的配方相关领域事件消费边界。
/// </summary>
public interface IRecipeEventConsumer :
    ISchedulerDomainEventConsumer<RecipeAssignedEvent>,
    ISchedulerDomainEventConsumer<RecipeChangedEvent>
{
}
