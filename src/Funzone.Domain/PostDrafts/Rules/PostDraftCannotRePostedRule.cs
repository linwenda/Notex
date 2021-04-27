using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PostDrafts.Rules
{
    public class PostDraftCannotRePostedRule : IBusinessRule
    {
        private readonly bool _isPosted;

        public PostDraftCannotRePostedRule(bool isPosted)
        {
            _isPosted = isPosted;
        }

        public bool IsBroken() => _isPosted;

        public string Message => "Post successfully created.";
    }
}