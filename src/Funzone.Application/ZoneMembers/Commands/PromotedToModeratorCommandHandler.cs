using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Application.ZoneMembers.Commands
{
    public class PromotedToModeratorCommandHandler : ICommandHandler<PromotedToModeratorCommand, bool>
    {
        private readonly IZoneMemberRepository _zoneMemberRepository;

        public PromotedToModeratorCommandHandler(IZoneMemberRepository zoneMemberRepository)
        {
            _zoneMemberRepository = zoneMemberRepository;
        }

        public async Task<bool> Handle(PromotedToModeratorCommand request, CancellationToken cancellationToken)
        {
            var destMember = await _zoneMemberRepository.GetByIdAsync(new ZoneMemberId(request.MemberId));
            
            var currentMember = await _zoneMemberRepository.GetCurrentMember(destMember.ZoneId);

            destMember.PromotedToModerator(currentMember);

            return await _zoneMemberRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}