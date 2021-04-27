using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Posts;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.Zones;

namespace Funzone.Application.Commands.Posts
{
    public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, bool>
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserContext _userContext;

        public CreatePostCommandHandler(
            IZoneRepository zoneRepository,
            IZoneMemberRepository zoneMemberRepository,
            IPostRepository postRepository,
            IUserContext userContext)
        {
            _zoneRepository = zoneRepository;
            _zoneMemberRepository = zoneMemberRepository;
            _postRepository = postRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.GetByIdAsync(new ZoneId(request.ZoneId));

            var zoneMember = await _zoneMemberRepository.FindAsync(zone.Id, _userContext.UserId);

            var post = zone.AddPost(zoneMember, request.Title, request.Content, request.Type);

            await _postRepository.AddAsync(post);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}