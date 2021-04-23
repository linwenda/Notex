using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.SharedKernel.Rules;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneCanBeEditedOnlyByAuthorRule : CanBeOperatedOnlyByAuthorRule
    {
        public ZoneCanBeEditedOnlyByAuthorRule(IHaveAuthorId author, UserId userId) : base(author, userId)
        {
        }

        public override string Message => "Only the author of a zone can edit it.";
    }
}