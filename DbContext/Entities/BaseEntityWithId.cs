namespace DbContext.Entities;

public abstract class BaseEntityWithId<TId> : BaseEntity, IObjectWithId<TId>, IUpdatable
    where TId : struct
{
    public TId Id { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
}