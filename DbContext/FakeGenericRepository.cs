using System.Linq.Expressions;

namespace DbContext;

public class FakeGenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly List<TEntity> _list = [];
    
    public async Task CreateAsync(TEntity item)
    {
        _list.Add(item);
        await Task.FromResult(0);
    }

    public async Task CreateRangeAsync(IEnumerable<TEntity> items)
    {
        _list.AddRange(items);
        await Task.FromResult(0);
    }

    public void Create(TEntity item)
    {
        _list.Add(item);
    }

    public void CreateRange(IEnumerable<TEntity> items)
    {
        _list.AddRange(items);
    }

    public void Update(TEntity item)
    {
        _list.Add(item);
    }

    public void Delete(TEntity item) => _list.Remove(item);

    public IAsyncEnumerable<TEntity> GetAllAsync(bool tracking = false) => _list.ToAsyncEnumerable();

    public IAsyncEnumerable<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false) 
        => _list.Where(predicate.Compile()).ToAsyncEnumerable();

    public IQueryable<TEntity> Query(bool tracking = false) => _list.AsQueryable();

    public IQueryable<TEntity> SqlQuery(FormattableString sql)
    {
        throw new NotImplementedException();
    }
}