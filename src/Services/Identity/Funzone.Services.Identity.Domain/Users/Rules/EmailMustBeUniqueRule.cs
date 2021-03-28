using Funzone.BuildingBlocks.Domain;

namespace Funzone.Services.Identity.Domain.Users.Rules
{
    public class EmailMustBeUniqueRule : IBusinessRule
    {
        private readonly IUserCounter _userCounter;
        private readonly EmailAddress _emailAddress;

        public EmailMustBeUniqueRule(
            IUserCounter userCounter,
            EmailAddress emailAddress)
        {
            _userCounter = userCounter;
            _emailAddress = emailAddress;
        }

        public bool IsBroken()
        {
            return _userCounter.CountUsersWithEmailAddress(_emailAddress) > 0;
        }

        public string Message => "User with this email already exists.";
    }
}