using Funzone.Domain.SeedWork;

namespace Funzone.Domain.ZoneUsers.Rules
{
    public class ZoneUserCannotRejoinRule : IBusinessRule
    {
        private readonly bool _isLeft;

        public ZoneUserCannotRejoinRule(bool isLeft)
        {
            _isLeft = isLeft;
        }

        public bool IsBroken() => !_isLeft;

        public string Message => "You have joined successfully.";
    }
}