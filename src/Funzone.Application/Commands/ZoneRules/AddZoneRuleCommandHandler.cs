using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.ZoneRules;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.ZoneRules
{
    public class AddZoneRuleCommandHandler : ICommandHandler<AddZoneRuleCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;

        public AddZoneRuleCommandHandler(
            IZoneRepository zoneRepository,
            IZoneMemberRepository zoneMemberRepository,
            IZoneRuleRepository zoneRuleRepository)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _zoneRuleRepository = zoneRuleRepository;
        }

        public async Task<bool> Handle(AddZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            var zoneMember = await _zoneMemberRepository.GetCurrentMember(zone.Id);

            var zoneRule = zone.AddRule(zoneMember, request.Title, request.Description, request.Sort);

            await _zoneRuleRepository.AddAsync(zoneRule);
            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}