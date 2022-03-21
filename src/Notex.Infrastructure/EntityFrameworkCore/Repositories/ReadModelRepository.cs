using Notex.Core.Aggregates;
using Notex.Core.Exceptions;
using Notex.Core.Lifetimes;

namespace Notex.Infrastructure.EntityFrameworkCore.Repositories;

public class EfCoreReadModelRepository : IReadModelRepository, IScopedLifetime
{
    private readonly NotexDbContext _dbContext;

    public EfCoreReadModelRepository(NotexDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> GetAsync<TEntity>(object id) where TEntity : class, IReadModelEntity
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IReadModelEntity
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IReadModelEntity
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IReadModelEntity
    {
        _dbContext.Set<TEntity>().Update(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IReadModelEntity
    {
        _dbContext.Set<TEntity>().Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}