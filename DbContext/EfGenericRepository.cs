using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DbContext;

public class EfGenericRepository<TEntity>(Microsoft.EntityFrameworkCore.DbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    
    public async Task CreateAsync(TEntity item) => await _dbSet.AddAsync(item);

    public async Task CreateRangeAsync(IEnumerable<TEntity> items) => await _dbSet.AddRangeAsync(items);

    public void Create(TEntity item) => _dbSet.Add(item);

    public void CreateRange(IEnumerable<TEntity> items) => _dbSet.AddRange(items);

    public void Update(TEntity item) => _dbSet.Add(item);

    public void Delete(TEntity item) => _dbSet.Remove(item);

    
    public IAsyncEnumerable<TEntity> GetAllAsync(bool tracking = false) =>
        (tracking ? _dbSet : _dbSet.AsNoTracking()).AsAsyncEnumerable();

    public IAsyncEnumerable<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
        => (tracking ? _dbSet : _dbSet.AsNoTracking()).Where(predicate).AsAsyncEnumerable();

    
    public IQueryable<TEntity> Query(bool tracking = false) =>
        (tracking ? _dbSet.AsQueryable() : _dbSet.AsNoTracking()).TagWithCallSite();

    public IQueryable<TEntity> SqlQuery(FormattableString sql)
        => _dbSet.FromSql(sql).TagWithCallSite();
}