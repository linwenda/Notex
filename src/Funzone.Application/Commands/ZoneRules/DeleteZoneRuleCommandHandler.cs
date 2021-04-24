using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneRules;
using Funzone.Domain.ZoneUsers;

namespace Funzone.Application.Commands.ZoneRules
{
    public class DeleteZoneRuleCommandHandler : ICommandHandler<DeleteZoneRuleCommand, bool>
    {
        private readonly IZoneUserRepository _zoneUserRepository;
        private readonly IZoneRuleRepository _zoneRuleRepository;
        private readonly IUserContext _userContext;

        public DeleteZoneRuleCommandHandler(
            IZoneUserRepository zoneUserRepository,
            IZoneRuleRepository zoneRuleRepository,
            IUserContext userContext)
        {
            _zoneUserRepository = zoneUserRepository;
            _zoneRuleRepository = zoneRuleRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(DeleteZoneRuleCommand request, CancellationToken cancellationToken)
        {
            var zoneRule = await _zoneRuleRepository.GetByIdAsync(new ZoneRuleId(request.ZoneRuleId));

            var zoneUser = await _zoneUserRepository.GetAsync(zoneRule.ZoneId, _userContext.UserId);

            zoneRule.Delete(zoneUser);

            return await _zoneRuleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}