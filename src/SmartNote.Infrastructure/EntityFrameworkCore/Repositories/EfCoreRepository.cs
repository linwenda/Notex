using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartNote.Core.Domain;

namespace SmartNote.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfCoreRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly SmartNoteDbContext _context;

        public EfCoreRepository(SmartNoteDbContext context)
        {
            _context = context;
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity is ICanSoftDelete softDeleteEntity)
            {
                softDeleteEntity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>().SingleOrDefaultAsync(predicate);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T));
            }

            return entity;
        }

        public async Task<T> GetAsync(object id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T), id);
            }

            return entity;
        }

        public IQueryable<T> Queryable => _context.Set<T>();
    }
}