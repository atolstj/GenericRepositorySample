namespace DbContext.Entities;

public interface IObjectWithId<TId>
    where TId : struct // целочисленные типы или уникальный идентификатор, строковое поле Id называть не следует
{
    TId Id { get; set; }
}