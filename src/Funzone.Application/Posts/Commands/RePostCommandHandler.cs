using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;

namespace Funzone.Application.Posts.Commands
{
    public class RePostCommandHandler : ICommandHandler<RePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public RePostCommandHandler(
            IPostRepository postRepository,
            IUserContext userContext)
        {
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(RePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            post.RePost(_userContext.UserId);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}