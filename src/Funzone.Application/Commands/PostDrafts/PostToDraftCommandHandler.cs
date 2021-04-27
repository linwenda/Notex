using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Application.Commands.PostDrafts
{
    public class PostToDraftCommandHandler : ICommandHandler<PostToDraftCommand, bool>
    {
        private readonly IPostDraftRepository _postDraftRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public PostToDraftCommandHandler(
            IPostDraftRepository postDraftRepository,
            IZoneMemberRepository zoneMemberRepository,
            IPostRepository postRepository,
            IUserContext userContext)
        {
            _postDraftRepository = postDraftRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(PostToDraftCommand request, CancellationToken cancellationToken)
        {
            var draft = await _postDraftRepository.GetByIdAsync(new PostDraftId(request.PostDraftId));

            var member = await _zoneMemberRepository.FindAsync(draft.ZoneId, _userContext.UserId);

            var post = draft.Post(member);

            await _postRepository.AddAsync(post);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}