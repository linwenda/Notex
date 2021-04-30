using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.Posts;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Application.PostDrafts.Commands
{
    public class AddPostDraftCommandHandler : ICommandHandler<AddPostDraftCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IPostDraftRepository _postDraftRepository;

        public AddPostDraftCommandHandler(
            IZoneRepository zoneRepository,
            IZoneMemberRepository zoneMemberRepository,
            IPostDraftRepository postDraftRepository)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _postDraftRepository = postDraftRepository;
        }

        public async Task<bool> Handle(AddPostDraftCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            var zoneMember = await _zoneMemberRepository.GetCurrentMember(zone.Id);

            var draft = zone.AddPostDraft(zoneMember, request.Title, request.Content,PostType.Of(request.Type));

            await _postDraftRepository.AddAsync(draft);

            return await _postDraftRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}