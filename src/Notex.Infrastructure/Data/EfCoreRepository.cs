using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notex.Core.DependencyInjection;
using Notex.Core.Domain.SeedWork;
using Notex.Core.Exceptions;

namespace Notex.Infrastructure.Data;

public class EfCoreRepository<TEntity> : IRepository<TEntity>, IScopedLifetime where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _dbContext;

    public EfCoreRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Set<TEntity>()
            .FindAsync(new[] {id}, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity), id);
        }

        return entity;
    }

    public async Task<IReadOnlyCollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().CountAsync(predicate, cancellationToken);
    }

    public IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Update(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TEntity>().Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}