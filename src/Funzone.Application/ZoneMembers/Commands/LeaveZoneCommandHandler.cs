using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class LeaveZoneCommandHandler : ICommandHandler<LeaveZoneCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;

        public LeaveZoneCommandHandler(IZoneMemberRepository zoneMemberRepository)
        {
            _zoneMemberRepository = zoneMemberRepository;
        }

        public async Task<bool> Handle(LeaveZoneCommand request, CancellationToken cancellationToken)
        {
            var zoneMember = await _zoneMemberRepository.GetCurrentMember(new ZoneId(request.ZoneId));
            if (zoneMember == null)
            {
                throw new NotFoundException(nameof(ZoneMember), request.ZoneId);
            }

            zoneMember.Leave();

            return await _zoneMemberRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}