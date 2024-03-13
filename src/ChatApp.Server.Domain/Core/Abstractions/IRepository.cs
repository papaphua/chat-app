namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IRepository<in TEntity>
    where TEntity : IEntity
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(params TEntity[] entities);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void UpdateRange(params TEntity[] entities);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(params TEntity[] entities);

    void RemoveRange(IEnumerable<TEntity> entities);
}