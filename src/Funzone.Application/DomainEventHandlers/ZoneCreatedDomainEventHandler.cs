using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Zones.Events;
using Funzone.Domain.ZoneUsers;
using MediatR;

namespace Funzone.Application.DomainEventHandlers
{
    public class ZoneCreatedDomainEventHandler : INotificationHandler<ZoneCreatedDomainEvent>
    {
        private readonly IZoneUserRepository _zoneUserRepository;

        public ZoneCreatedDomainEventHandler(
            IZoneUserRepository zoneUserRepository)
        {
            _zoneUserRepository = zoneUserRepository;
        }

        public async Task Handle(ZoneCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var administrator = notification.Zone.AddAdministrator();

            await _zoneUserRepository.AddAsync(administrator);
            await _zoneUserRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}