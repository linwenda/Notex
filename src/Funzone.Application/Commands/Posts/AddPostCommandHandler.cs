using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.Posts
{
    public class AddPostCommandHandler : ICommandHandler<AddPostCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public AddPostCommandHandler(
            IZoneRepository zoneRepository,
            IPostRepository postRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            var post = zone.AddPost(_userContext.UserId, request.Title, request.Content, PostType.Of(request.Type));

            await _postRepository.AddAsync(post);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}