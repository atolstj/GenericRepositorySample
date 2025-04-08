using System.Diagnostics;
using DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbContext;

public abstract class EfDbContext(IConfiguration configuration) : Microsoft.EntityFrameworkCore.DbContext, IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnectionString"))
            .LogTo(message => Debug.WriteLine(message));
    }

    public async Task SaveAllAsync()
    {
        await SaveChangesAsync();
    }

    IGenericRepository<TEntity> IUnitOfWork.Repository<TEntity>()
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (_repositories[typeof(TEntity)] as IGenericRepository<TEntity>)!;
        }   

        var repository = new EfGenericRepository<TEntity>(this);

        _repositories.Add(typeof(TEntity), repository);

        return repository;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // DetectChanges() вызываем 1 раз вручную
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.DetectChanges();

        // Устанавливаем значения полей Created и Updated
        var dtNow = DateTimeOffset.UtcNow;
        SetCreated(dtNow);
        SetUpdated(dtNow);

        var result = await base.SaveChangesAsync(cancellationToken);
        
        // Детачим уже созданные неизменяемые сущности во избежании квадратичной деградации при обработке batch'ей
        DetachNonUpdatable();
        
        return result;
    }

    private void SetCreated(DateTimeOffset dtNow)
    {
        var added = ChangeTracker.Entries()
            .Where(d => d.State == EntityState.Added);

        foreach (var entry in added)
        {
            entry.CurrentValues[nameof(BaseEntity.CreatedAt)] = dtNow;
        }
    }

    private void SetUpdated(DateTimeOffset dtNow)
    {
        var modified = ChangeTracker.Entries()
            .Where(d => d is { State: EntityState.Modified, Entity: IUpdatable });

        foreach (var entry in modified)
        {
            entry.CurrentValues[nameof(IUpdatable.UpdatedAt)] = dtNow;
        }
    }

    private void DetachNonUpdatable()
    {
        var entries = ChangeTracker.Entries()
            .Where(d => d.State != EntityState.Unchanged && d.Entity is not IUpdatable);

        foreach (var entry in entries)
        {
            entry.State = EntityState.Detached;
        }
    }
}