using DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbContext;

public static class GenericRepositoryExtensions
{
    public static async Task<TEntity> GetByIdAsync<TEntity, TId>(this IGenericRepository<TEntity> repo, TId id)
        where TEntity : class, IObjectWithId<TId>
        where TId : struct
    {
        var entity = await repo.Query(true).SingleOrDefaultAsync(d => d.Id.Equals(id));
        
        if(entity == null)
            throw new KeyNotFoundException($"Сущность {nameof(TEntity)} с Id = {id} не найдена");
        
        return entity;
    }
}