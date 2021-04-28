using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;

namespace Funzone.Application.Commands.Posts
{
    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand,bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public DeletePostCommandHandler(IPostRepository postRepository,IUserContext userContext)
        {
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            post.Delete(_userContext.UserId);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}