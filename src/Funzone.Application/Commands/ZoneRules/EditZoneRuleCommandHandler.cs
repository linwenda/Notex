using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneRules;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Application.Commands.ZoneRules
{
    public class EditZoneRuleCommandHandler : ICommandHandler<EditZoneRuleCommand, bool>
    {
        private readonly IZoneUserRepository _zoneUserRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;
        private readonly IUserContext _userContext;

        public EditZoneRuleCommandHandler(
            IZoneUserRepository zoneUserRepository,
            IZoneRuleRepository zoneRuleRepository,
            IUserContext userContext)
        {
            _zoneUserRepository = zoneUserRepository;
            _zoneRuleRepository = zoneRuleRepository;
            _userContext = userContext;
        }
        
        public async Task<bool> Handle(EditZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zoneRule = await _zoneRuleRepository.GetByIdAsync(new ZoneRuleId(request.ZoneRuleId));

            var zoneUser = await _zoneUserRepository.GetAsync(zoneRule.ZoneId, _userContext.UserId);

            zoneRule.Edit(zoneUser, request.Title, request.Description, request.Sort);

            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}