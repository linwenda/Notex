using System.Linq.Expressions;

namespace SmartNote.Domain
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAsync(object id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Queryable { get; }
        Task InsertAsync(T entity);
        Task InsertManyAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateManyAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteManyAsync(IEnumerable<T> entities);
    }
}