using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class PromoteToModeratorCommandHandler : ICommandHandler<PromoteToModeratorCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;

        public PromoteToModeratorCommandHandler(IZoneMemberRepository zoneMemberRepository)
        {
            _zoneMemberRepository = zoneMemberRepository;
        }

        public async Task<bool> Handle(PromoteToModeratorCommand request, CancellationToken cancellationToken)
        {
            var zoneMember = await _zoneMemberRepository.GetByIdAsync(new ZoneMemberId(request.MemberId));

            if (zoneMember == null)
            {
                throw new NotFoundException(nameof(ZoneMember), request.MemberId);
            }

            var currentMember = await _zoneMemberRepository.GetCurrentMember(zoneMember.ZoneId);

            zoneMember.PromoteToModerator(currentMember);

            return await _zoneMemberRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}