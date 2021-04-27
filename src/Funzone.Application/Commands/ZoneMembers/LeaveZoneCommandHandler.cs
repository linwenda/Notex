using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.ZoneMembers
{
    public class LeaveZoneCommandHandler : ICommandHandler<LeaveZoneCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IUserContext _userContext;

        public LeaveZoneCommandHandler(IZoneMemberRepository zoneMemberRepository, IUserContext userContext)
        {
            _zoneMemberRepository = zoneMemberRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(LeaveZoneCommand request, CancellationToken cancellationToken)
        {
            var zoneMember = await _zoneMemberRepository.FindAsync(new ZoneId(request.ZoneId), _userContext.UserId);
            if (zoneMember == null)
            {
                throw new NotFoundException(nameof(ZoneMember), request.ZoneId);
            }

            zoneMember.Leave();

            return await _zoneMemberRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}