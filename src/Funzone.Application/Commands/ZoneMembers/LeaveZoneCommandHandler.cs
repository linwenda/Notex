using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.ZoneUsers
{
    public class LeaveZoneCommandHandler : ICommandHandler<LeaveZoneCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneUserRepository;
        private readonly IUserContext _userContext;

        public LeaveZoneCommandHandler(IZoneMemberRepository zoneUserRepository, IUserContext userContext)
        {
            _zoneUserRepository = zoneUserRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(LeaveZoneCommand request, CancellationToken cancellationToken)
        {
            var zoneUser = await _zoneUserRepository.GetAsync(new ZoneId(request.ZoneId), _userContext.UserId);
            if (zoneUser == null)
            {
                throw new NotFoundException(nameof(ZoneUser), request.ZoneId);
            }
            
            zoneUser.Leave();

            return await _zoneUserRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}