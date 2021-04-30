using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones.Events;
using MediatR;

namespace Funzone.Application.Zones.DomainEventHandlers
{
    public class ZoneCreatedDomainEventHandler : INotificationHandler<ZoneCreatedDomainEvent>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;

        public ZoneCreatedDomainEventHandler(
            IZoneMemberRepository zoneMemberRepository)
        {
            _zoneMemberRepository = zoneMemberRepository;
        }

        public async Task Handle(ZoneCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var administrator = notification.Zone.AddAdministrator();

            await _zoneMemberRepository.AddAsync(administrator);
            await _zoneMemberRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}