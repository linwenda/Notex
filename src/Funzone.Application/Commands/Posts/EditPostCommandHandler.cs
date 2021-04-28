using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;

namespace Funzone.Application.Commands.Posts
{
    public class EditPostCommandHandler : ICommandHandler<EditPostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public EditPostCommandHandler(IPostRepository postRepository,IUserContext userContext)
        {
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            post.Edit(_userContext.UserId, request.Title, request.Content);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}