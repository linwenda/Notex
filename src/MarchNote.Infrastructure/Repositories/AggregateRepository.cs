using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.SeedWork.Aggregates;
using MarchNote.Infrastructure.Events;
using MarchNote.Infrastructure.EventStore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MarchNote.Infrastructure.Repositories
{
    public class AggregateRepository<TAggregate, TAggregateId> : IAggregateRepository<TAggregate, TAggregateId>
        where TAggregate : IAggregateRoot<TAggregateId>
        where TAggregateId : IAggregateId
    {
        private readonly IMediator _mediator;
        private readonly MarchNoteDbContext _context;

        protected AggregateRepository(
            MarchNoteDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<TAggregate> LoadAsync(TAggregateId aggregateId, CancellationToken cancellationToken = default)
        {
            return await LoadAsync(aggregateId, int.MaxValue, cancellationToken);
        }

        public async Task<TAggregate> LoadAsync(TAggregateId aggregateId, int version,
            CancellationToken cancellationToken = default)
        {
            var maxVersion = version <= 0 ? int.MaxValue : version;

            var snapshot = await GetLatestSnapshot(aggregateId, maxVersion);

            var minVersion = snapshot?.AggregateVersion + 1 ?? 0;

            var events = await GetDomainEvents(aggregateId, minVersion, maxVersion);

            var aggregate = CreateAggregateRoot(aggregateId);

            if (snapshot == null)
            {
                if (events.Count == 0)
                {
                    return default;
                }
                
                aggregate.Load(events);
            }
            else
            {
                aggregate.Load(snapshot, events);
            }

            return aggregate;
        }


        public async Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
        {
            var uncommittedDomainEvents = aggregate.GetUnCommittedEvents();

            if (uncommittedDomainEvents.Any())
            {
                var eventEntities = uncommittedDomainEvents.Select(e => new EventEntity
                {
                    AggregateId = aggregate.Id.Value,
                    AggregateVersion = aggregate.Version,
                    Timestamp = DateTime.UtcNow,
                    Type = e.GetType().FullName,
                    Data = JsonConvert.SerializeObject(e)
                });

                await _context.Set<EventEntity>().AddRangeAsync(eventEntities, cancellationToken);
            }

            var uncommittedSnapshot = aggregate.GetUnCommittedSnapshot();
            if (uncommittedSnapshot != null)
            {
                await _context.Set<SnapshotEntity>().AddAsync(new SnapshotEntity
                {
                    AggregateId = aggregate.Id.Value,
                    AggregateVersion = aggregate.Version,
                    Type = uncommittedSnapshot.GetType().FullName,
                    Data = JsonConvert.SerializeObject(uncommittedSnapshot),
                }, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var uncommittedDomainEvent in uncommittedDomainEvents)
            {
                await _mediator.Publish(uncommittedDomainEvent, cancellationToken);
            }
        }

        private static TAggregate CreateAggregateRoot(TAggregateId aggregateId)
        {
            var args = new object[] {aggregateId};
            var aggregateRoot = (TAggregate) Activator.CreateInstance(typeof(TAggregate),
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, args,
                CultureInfo.CurrentCulture);

            return aggregateRoot;
        }

        private async Task<List<IDomainEvent>> GetDomainEvents(TAggregateId aggregateId, int minVersion, int maxVersion)
        {
            var assembly = typeof(IDomainEvent).Assembly;

            var events = await _context.Set<EventEntity>()
                .Where(x => x.AggregateId == aggregateId.Value)
                .Where(x => x.AggregateVersion >= minVersion)
                .Where(x => x.AggregateVersion <= maxVersion)
                .OrderBy(x => x.AggregateVersion)
                .ToListAsync()
                .ConfigureAwait(false);

            return events.Select(e => JsonConvert.DeserializeObject(e.Data,
                assembly.GetType(e.Type)) as IDomainEvent).ToList();
        }

        private async Task<ISnapshot> GetLatestSnapshot(TAggregateId aggregateId, int maxVersion)
        {
            var assembly = typeof(ISnapshot).Assembly;

            var snapshot = await _context.Set<SnapshotEntity>()
                .Where(x => x.AggregateId == aggregateId.Value)
                .Where(x => x.AggregateVersion <= maxVersion)
                .OrderByDescending(x => x.AggregateVersion)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return snapshot == null
                ? null
                : JsonConvert.DeserializeObject(snapshot.Data, assembly.GetType(snapshot.Type)) as ISnapshot;
        }
    }
}