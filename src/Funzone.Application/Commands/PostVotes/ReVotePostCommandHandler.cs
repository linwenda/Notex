using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.PostVotes;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;

namespace Funzone.Application.Commands.PostVotes
{
    public class ReVotePostCommandHandler : ICommandHandler<ReVotePostCommand, bool>
    {
        private readonly IPostVoteRepository _postVoteRepository;
        private readonly IUserContext _userContext;

        public ReVotePostCommandHandler(IPostVoteRepository postVoteRepository, IUserContext userContext)
        {
            _postVoteRepository = postVoteRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(ReVotePostCommand request, CancellationToken cancellationToken)
        {
            var postVote = await _postVoteRepository.GetByIdAsync(new PostVoteId(request.VoteId));

            postVote.ReVote(_userContext.UserId, VoteType.Of(request.VoteType));

            return await _postVoteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}