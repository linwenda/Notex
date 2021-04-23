using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Application.Commands.ZoneUsers
{
    public class JoinZoneCommandHandler : ICommandHandler<JoinZoneCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneUserRepository _zoneUserRepository;
        private readonly IUserContext _userContext;

        public JoinZoneCommandHandler(
            IZoneRepository zoneRepository,
            IZoneUserRepository zoneUserRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _zoneUserRepository = zoneUserRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(JoinZoneCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));
            
            var zoneUser = await _zoneUserRepository.GetAsync(zone.Id, _userContext.UserId);

            if (zoneUser == null)
            {
                zoneUser = zone.Join(_userContext.UserId);
                await _zoneUserRepository.AddAsync(zoneUser);
            }
            else
            {
                zoneUser.Rejoin();
            }

            return await _zoneUserRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}