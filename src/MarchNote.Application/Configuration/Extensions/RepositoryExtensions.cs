using System.Threading.Tasks;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Application.Configuration.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<TEntity> CheckNotNull<TEntity>(this IRepository<TEntity> repository, object id)
            where TEntity : IEntity
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(typeof(TEntity), id);
            }

            return entity;
        }
    }
}