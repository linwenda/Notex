using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.ZoneRules;

namespace Funzone.Application.ZoneRules.Commands
{
    public class DeleteZoneRuleCommandHandler : ICommandHandler<DeleteZoneRuleCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;

        public DeleteZoneRuleCommandHandler(
            IZoneMemberRepository zoneMemberRepository,
            IZoneRuleRepository zoneRuleRepository)
        {
            _zoneMemberRepository = zoneMemberRepository;
            _zoneRuleRepository = zoneRuleRepository;
        }

        public async Task<bool> Handle(DeleteZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zoneRule = await _zoneRuleRepository.GetByIdAsync(new ZoneRuleId(request.ZoneRuleId));

            var zoneMember = await _zoneMemberRepository.GetCurrentMember(zoneRule.ZoneId);

            zoneRule.Delete(zoneMember);

            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}