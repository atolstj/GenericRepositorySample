namespace DbContext.Entities;

public abstract class BaseEntityWithIdUpdatable<TId> : BaseEntityWithId<TId>
    where TId : struct
{
    public DateTime? UpdatedAt { get; set; }
}