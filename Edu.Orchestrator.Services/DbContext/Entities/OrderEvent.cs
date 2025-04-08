using DbContext.Entities;

namespace Edu.Orchestrator.Services.DbContext.Entities;

public abstract class OrderEvent : BaseEntityWithId<int>
{
    public int UserProductId { get; init; }
}