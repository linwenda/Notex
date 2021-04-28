using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.PostDrafts.Rules
{
    public class PostDraftCanBeDeletedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _editorId;

        public PostDraftCanBeDeletedOnlyByAuthorRule(UserId authorId, UserId editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }

        public bool IsBroken() => _authorId != _editorId;

        public string Message => "Only author can edit draft.";
    }
}