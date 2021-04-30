using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.ZoneRules;

namespace Funzone.Application.ZoneRules.Commands
{
    public class EditZoneRuleCommandHandler : ICommandHandler<EditZoneRuleCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;

        public EditZoneRuleCommandHandler(
            IZoneMemberRepository zoneMemberRepository,
            IZoneRuleRepository zoneRuleRepository)
        {
            _zoneMemberRepository = zoneMemberRepository;
            _zoneRuleRepository = zoneRuleRepository;
        }
        
        public async Task<bool> Handle(EditZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zoneRule = await _zoneRuleRepository.GetByIdAsync(new ZoneRuleId(request.ZoneRuleId));

            var zoneMember = await _zoneMemberRepository.GetCurrentMember(zoneRule.ZoneId);

            zoneRule.Edit(zoneMember, request.Title, request.Description, request.Sort);

            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}