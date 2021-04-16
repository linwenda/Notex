using Funzone.Domain.Users;
using NSubstitute;

namespace Funzone.UnitTests.Users
{
    public class UserTestBase : TestBase
    {
        protected class UserTestDataOptions
        {
            internal User User { get; set; }
        }

        protected class UserTestData
        {
            public UserTestData(User user)
            {
                User = user;
            }

            internal User User { get; }
        }

        protected UserTestData CreateUserTestData(UserTestDataOptions options)
        {
            var userChecker = Substitute.For<IUserChecker>();
            userChecker.IsUnique(Arg.Any<EmailAddress>())
                .Returns(true);

            var user = options.User ?? User.RegisterByEmail(userChecker,
                new EmailAddress("test@outlook.com"),
                "passwordSalt",
                "passwordHash");

            return new UserTestData(user);
        }
    }
}