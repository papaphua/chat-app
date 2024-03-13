using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Core.Abstractions;

public abstract class Repository<TEntity>(ApplicationDbContext dbContext)
    where TEntity : class, IEntity
{
    public async Task AddAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>()
            .AddAsync(entity);
    }

    public async Task AddRangeAsync(params TEntity[] entities)
    {
        await dbContext.Set<TEntity>()
            .AddRangeAsync(entities);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await dbContext.Set<TEntity>()
            .AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>()
            .Update(entity);
    }

    public void UpdateRange(params TEntity[] entities)
    {
        dbContext.Set<TEntity>()
            .UpdateRange(entities);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>()
            .UpdateRange(entities);
    }

    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>()
            .Remove(entity);
    }

    public void RemoveRange(params TEntity[] entities)
    {
        dbContext.Set<TEntity>()
            .RemoveRange(entities);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>()
            .RemoveRange(entities);
    }
}