using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Contract;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using MediatR;

namespace Funzone.Application.Commands.Zones.CreateZone
{
    public class CreateZoneCommandHandler : ICommandHandler<CreateZoneCommand>
    {
        private readonly IZoneCounter _zoneCounter;
        private readonly IZoneRepository _zoneRepository;
        private readonly IUserContext _userContext;

        public CreateZoneCommandHandler(
            IZoneCounter zoneCounter,
            IZoneRepository zoneRepository,
            IUserContext userContext)
        {
            _zoneCounter = zoneCounter;
            _zoneRepository = zoneRepository;
            _userContext = userContext;
        }
        
        public async Task<Unit> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
        {
            var zone = Zone.Create(
                _zoneCounter,
                _userContext.UserId,
                request.Title,
                request.Description);

            await _zoneRepository.AddAsync(zone);   
            await _zoneRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}