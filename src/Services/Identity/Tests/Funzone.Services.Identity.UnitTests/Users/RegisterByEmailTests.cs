using Funzone.Services.Identity.Domain.Users;
using Funzone.Services.Identity.Domain.Users.Events;
using Funzone.Services.Identity.Domain.Users.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Funzone.Services.Identity.UnitTests.Users
{
    public class RegisterByEmailTests : TestBase
    {
        [Test]
        public void RegisterByEmail_ExistsEmail_BrokenEmailMustBeUniqueRule()
        {
            var userCounter = Substitute.For<IUserCounter>();
            userCounter.CountUsersWithEmailAddress(Arg.Any<EmailAddress>())
                .Returns(1);

            ShouldBrokenRule<EmailMustBeUniqueRule>(() =>
                User.RegisterByEmail(
                    userCounter,
                    new EmailAddress("test@outlook.com"),
                    "test",
                    "test"));
        }

        [Test]
        public void RegisterByEmail_WithUniqueEmail_Successful()
        {
            var userCounter = Substitute.For<IUserCounter>();

            var user = User.RegisterByEmail(
                userCounter,
                new EmailAddress("test@outlook.com"),
                "test",
                "test");

            ShouldBeOfDomainEvent<UserRegisteredDomainEvent>(user);
        }
    }
}