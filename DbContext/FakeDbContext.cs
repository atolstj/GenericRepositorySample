namespace DbContext;

public class FakeDbContext : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();
    
    IGenericRepository<TEntity> IUnitOfWork.Repository<TEntity>()
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (_repositories[typeof(TEntity)] as IGenericRepository<TEntity>)!;
        }

        var repository = new FakeGenericRepository<TEntity>();

        _repositories.Add(typeof(TEntity), repository);

        return repository;
    }   

    public async Task SaveAllAsync()
    {
        await Task.FromResult(0);
    }
}