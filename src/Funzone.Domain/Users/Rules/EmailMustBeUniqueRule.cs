using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Users.Rules
{
    public class EmailMustBeUniqueRule : IBusinessRule
    {
        private readonly IUserChecker _userChecker;
        private readonly EmailAddress _emailAddress;

        public EmailMustBeUniqueRule(
            IUserChecker userChecker,
            EmailAddress emailAddress)
        {
            _userChecker = userChecker;
            _emailAddress = emailAddress;
        }

        public bool IsBroken()
        {
            return !_userChecker.IsUnique(_emailAddress);
        }

        public string Message => "User with this email already exists.";
    }
}