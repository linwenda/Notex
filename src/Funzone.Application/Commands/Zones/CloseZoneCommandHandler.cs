using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using MediatR;

namespace Funzone.Application.Commands.Zones
{
    public class CloseZoneCommandHandler : ICommandHandler<CloseZoneCommand,bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IUserContext _userContext;

        public CloseZoneCommandHandler(IZoneRepository zoneRepository, IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(CloseZoneCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            zone.Close(_userContext.UserId);

            return await _zoneRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}