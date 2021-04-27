using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.ZoneMembers
{
    public class JoinZoneCommandHandler : ICommandHandler<JoinZoneCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IUserContext _userContext;

        public JoinZoneCommandHandler(
            IZoneRepository zoneRepository,
            IZoneMemberRepository zoneMemberRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(JoinZoneCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));
            
            var zoneMember = await _zoneMemberRepository.FindAsync(zone.Id, _userContext.UserId);

            if (zoneMember == null)
            {
                zoneMember = zone.Join(_userContext.UserId);
                await _zoneMemberRepository.AddAsync(zoneMember);
            }
            else
            {
                zoneMember.Rejoin();
            }

            return await _zoneMemberRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}