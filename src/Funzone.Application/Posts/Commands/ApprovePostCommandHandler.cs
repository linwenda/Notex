using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Posts;
using Funzone.Domain.ZoneMembers;

namespace Funzone.Application.Posts.Commands
{
    public class ApprovePostCommandHandler : ICommandHandler<ApprovePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IZoneMemberRepository _zoneMemberRepository;

        public ApprovePostCommandHandler(
            IPostRepository postRepository,
            IZoneMemberRepository zoneMemberRepository)
        {
            _postRepository = postRepository;
            _zoneMemberRepository = zoneMemberRepository;
        }

        public async Task<bool> Handle(ApprovePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(new PostId(request.PostId));

            var member = await _zoneMemberRepository.GetCurrentMember(post.ZoneId);

            post.Approve(member);

            return await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}