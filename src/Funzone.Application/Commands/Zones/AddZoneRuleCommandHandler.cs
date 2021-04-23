using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;
using MediatR;

namespace Funzone.Application.Commands.Zones
{
    public class AddZoneRuleCommandHandler : ICommandHandler<AddZoneRuleCommand,bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneUserRepository _zoneMemberRepository;
        private readonly IUserContext _userContext;

        public AddZoneRuleCommandHandler(
            IZoneRepository zoneRepository,
            IZoneUserRepository zoneMemberRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(AddZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));
            
            var zoneMember = await _zoneMemberRepository.GetAsync(zone.Id, _userContext.UserId);
            
            zone.AddRule(zoneMember, request.Title, request.Description);

            return await _zoneRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}