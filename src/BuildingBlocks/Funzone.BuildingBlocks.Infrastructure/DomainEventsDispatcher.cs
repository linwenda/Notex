using Funzone.BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Funzone.BuildingBlocks.Infrastructure
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly DbContext _context;
        private readonly IMediator _mediator;


        public DomainEventsDispatcher(
            DbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var publishEventTasks = domainEvents.Select(async e => await _mediator.Publish(e));
            await Task.WhenAll(publishEventTasks);
        }
    }
}
