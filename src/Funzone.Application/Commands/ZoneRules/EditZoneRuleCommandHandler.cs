using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.ZoneRules;

namespace Funzone.Application.Commands.ZoneRules
{
    public class EditZoneRuleCommandHandler : ICommandHandler<EditZoneRuleCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;
        private readonly IUserContext _userContext;

        public EditZoneRuleCommandHandler(
            IZoneMemberRepository zoneMemberRepository,
            IZoneRuleRepository zoneRuleRepository,
            IUserContext userContext)
        {
            _zoneMemberRepository = zoneMemberRepository;
            _zoneRuleRepository = zoneRuleRepository;
            _userContext = userContext;
        }
        
        public async Task<bool> Handle(EditZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zoneRule = await _zoneRuleRepository.GetByIdAsync(new ZoneRuleId(request.ZoneRuleId));

            var zoneMember = await _zoneMemberRepository.FindAsync(zoneRule.ZoneId, _userContext.UserId);

            zoneRule.Edit(zoneMember, request.Title, request.Description, request.Sort);

            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}