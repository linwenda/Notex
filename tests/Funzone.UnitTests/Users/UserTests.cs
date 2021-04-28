using Funzone.Domain.Users;
using Funzone.Domain.Users.Events;
using Funzone.Domain.Users.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Funzone.UnitTests.Users
{
    public class UserTests : UserTestBase
    {
        [Test]
        public void RegisterByEmail_ExistsEmail_BreakEmailMustBeUniqueRule()
        {
            var userCounter = Substitute.For<IUserChecker>();
            userCounter.IsUnique(Arg.Any<EmailAddress>()).Returns(false);

            ShouldBrokenRule<EmailMustBeUniqueRule>(() =>
                User.RegisterByEmail(
                    userCounter,
                    new EmailAddress("test@outlook.com"),
                    "test",
                    "test"));
        }

        [Test]
        public void RegisterByEmail_UniqueEmail_Successful()
        {
            var userCounter = Substitute.For<IUserChecker>();
            userCounter.IsUnique(Arg.Any<EmailAddress>()).Returns(true);

            var user = User.RegisterByEmail(
                userCounter,
                new EmailAddress("test@outlook.com"),
                "test",
                "test");

            ShouldAddedDomainEvent<UserRegisteredDomainEvent>(user);
        }
    }
}