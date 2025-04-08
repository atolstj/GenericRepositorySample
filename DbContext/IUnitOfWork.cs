using DbContext.Entities;

namespace DbContext;

public interface IUnitOfWork
{
    IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : class;

    Task SaveAllAsync();
}