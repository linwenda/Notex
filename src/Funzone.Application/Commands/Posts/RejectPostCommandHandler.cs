using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.Posts;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Application.Commands.Posts
{
    public class RejectPostCommandHandler : ICommandHandler<RejectPostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;

        public RejectPostCommandHandler(
            IPostRepository postRepository,
            IZoneMemberRepository zoneMemberRepository)
        {
            _postRepository = postRepository;
            _zoneMemberRepository = zoneMemberRepository;
        }

        public async Task<bool> Handle(RejectPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            var member = await _zoneMemberRepository.GetCurrentMember(post.ZoneId);

            post.Reject(member, request.Reason);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}