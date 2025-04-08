using System.Linq.Expressions;

namespace DbContext;

public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task CreateAsync(TEntity item); // асинхронность для получения Id сущности из сиквенса БД, если возможно
    Task CreateRangeAsync(IEnumerable<TEntity> items);
    void Create(TEntity item);
    void CreateRange(IEnumerable<TEntity> items);
    void Update(TEntity item);
    void Delete(TEntity item);
    
    IAsyncEnumerable<TEntity> GetAllAsync(bool tracking = false);
    IAsyncEnumerable<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false);

    IQueryable<TEntity> Query(bool tracking = false);
    IQueryable<TEntity> SqlQuery(FormattableString sql);
}