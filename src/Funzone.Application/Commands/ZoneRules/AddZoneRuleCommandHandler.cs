using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneRules;
using Funzone.Domain.Zones;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Application.Commands.ZoneRules
{
    public class AddZoneRuleCommandHandler : ICommandHandler<AddZoneRuleCommand,bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneUserRepository _zoneMemberRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;
        private readonly IUserContext _userContext;

        public AddZoneRuleCommandHandler(
            IZoneRepository zoneRepository,
            IZoneUserRepository zoneMemberRepository,
            IZoneRuleRepository zoneRuleRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _zoneRuleRepository = zoneRuleRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(AddZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));
            
            var zoneUser = await _zoneMemberRepository.GetAsync(zone.Id, _userContext.UserId);

            var zoneRule = zone.AddRule(zoneUser, request.Title, request.Description, request.Sort);

            await _zoneRuleRepository.AddAsync(zoneRule);
            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}