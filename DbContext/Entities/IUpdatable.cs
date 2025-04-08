namespace DbContext.Entities;

public interface IUpdatable
{
    public DateTimeOffset? UpdatedAt { get; set; }
}