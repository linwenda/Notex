using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.SharedKernel.Rules
{
    public class CanBeOperatedOnlyByAuthorRule : IBusinessRule
    {
        private readonly IHaveAuthorId _author;
        private readonly UserId _userId;

        protected CanBeOperatedOnlyByAuthorRule(IHaveAuthorId author, UserId userId)
        {
            _author = author;
            _userId = userId;
        }

        public bool IsBroken()
        {
            return _author.AuthorId != _userId;
        }

        public virtual string Message => "Only the author can operate it.";
    }
}