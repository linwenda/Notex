using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneMembers.Rules
{
    public class ZoneMemberCannotRejoinRule : IBusinessRule
    {
        private readonly bool _isLeft;

        public ZoneMemberCannotRejoinRule(bool isLeft)
        {
            _isLeft = isLeft;
        }

        public bool IsBroken() => !_isLeft;

        public string Message => "You have joined successfully.";
    }
}