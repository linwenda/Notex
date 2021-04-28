using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.PostDrafts
{
    public class AddPostDraftCommandHandler : ICommandHandler<AddPostDraftCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IPostDraftRepository _postDraftRepository;
        private readonly IUserContext _userContext;

        public AddPostDraftCommandHandler(
            IZoneRepository zoneRepository,
            IZoneMemberRepository zoneMemberRepository,
            IPostDraftRepository postDraftRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _postDraftRepository = postDraftRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(AddPostDraftCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            var zoneMember = await _zoneMemberRepository.FindAsync(zone.Id, _userContext.UserId);

            var draft = zone.AddPostDraft(zoneMember, request.Title, request.Content,PostType.Of(request.Type));

            await _postDraftRepository.AddAsync(draft);

            return await _postDraftRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}