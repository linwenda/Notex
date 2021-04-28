using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Posts;
using Funzone.Domain.PostVotes;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;

namespace Funzone.Application.Commands.PostVotes
{
    public class VotePostCommandHandler : ICommandHandler<VotePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostVoteRepository _postVoteRepository;
        private readonly IUserContext _userContext;

        public VotePostCommandHandler(
            IPostRepository postRepository,
            IPostVoteRepository postVoteRepository,
            IUserContext userContext)
        {
            _postRepository = postRepository;
            _postVoteRepository = postVoteRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(VotePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            post.Vote(_userContext.UserId, VoteType.Of(request.VoteType));

            return await _postVoteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}