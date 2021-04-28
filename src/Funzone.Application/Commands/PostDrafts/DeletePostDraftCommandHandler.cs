using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.PostDrafts.Rules;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Application.Commands.PostDrafts
{
    public class DeletePostDraftCommandHandler : ICommandHandler<DeletePostDraftCommand, bool>
    {
        private readonly IPostDraftRepository _postDraftRepository;
        private readonly IUserContext _userContext;

        public DeletePostDraftCommandHandler(IPostDraftRepository postDraftRepository,IUserContext userContext)
        {
            _postDraftRepository = postDraftRepository;
            _userContext = userContext;
        }

        public async Task<bool> Handle(DeletePostDraftCommand request, CancellationToken cancellationToken)
        {
            var postDraft = await _postDraftRepository.GetByIdAsync(new PostDraftId(request.PostDraftId));

            var rule = new PostDraftCanBeDeletedOnlyByAuthorRule(postDraft.AuthorId, _userContext.UserId);

            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }

            _postDraftRepository.Delete(postDraft);

            return await _postDraftRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}