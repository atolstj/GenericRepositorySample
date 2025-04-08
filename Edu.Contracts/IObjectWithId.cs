namespace Edu.Contracts;

public interface IObjectWithId<TId> where TId : struct
{
    TId Id { get; set; }
}