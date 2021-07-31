using System.Threading.Tasks;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Domain.SeedWork;

namespace MarchNote.Application.Configuration.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<T> FindAsync<T>(this IRepository<T> repository, TypedIdValueBase id) where T : IEntity
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(T), id.Value.ToString());
            }

            return entity;
        }
    }
}