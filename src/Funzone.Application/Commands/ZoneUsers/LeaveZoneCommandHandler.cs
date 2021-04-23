using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Application.Commands.ZoneUsers
{
    public class LeaveZoneCommandHandler : ICommandHandler<LeaveZoneCommand, bool>
    {
        private readonly IZoneUserRepository _zoneUserRepository;
        private readonly IUserContext _userContext;

        public LeaveZoneCommandHandler(IZoneUserRepository zoneUserRepository, IUserContext userContext)
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