using System.Linq;
using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Infrastructure.DataAccess;
using MediatR;

namespace Funzone.Infrastructure.Processing
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, FunzoneDbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var publishEventTasks = domainEvents.Select(e => mediator.Publish(e));
            await Task.WhenAll(publishEventTasks);
        }
    }
}