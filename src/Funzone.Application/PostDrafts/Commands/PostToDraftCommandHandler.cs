using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;

namespace Funzone.Application.PostDrafts.Commands
{
    public class PostToDraftCommandHandler : ICommandHandler<PostToDraftCommand, bool>
    {
        private readonly IPostDraftRepository _postDraftRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public PostToDraftCommandHandler(
            IPostDraftRepository postDraftRepository,
            IPostRepository postRepository,
            IUserContext userContext)
        {
            _postDraftRepository = postDraftRepository;
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(PostToDraftCommand request, CancellationToken cancellationToken)
        {
            var draft = await _postDraftRepository.GetByIdAsync(new PostDraftId(request.PostDraftId));

            var post = draft.Post(_userContext.UserId);

            await _postRepository.AddAsync(post);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}