using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Application.Zones.Commands
{
    public class EditZoneCommandHandler : ICommandHandler<EditZoneCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IUserContext _userContext;

        public EditZoneCommandHandler(IZoneRepository zoneRepository, IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(EditZoneCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            zone.Edit(_userContext.UserId, request.Description, request.AvatarUrl);

            return await _zoneRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}