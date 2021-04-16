using System.Threading.Tasks;
using Funzone.Application.Commands.Users.Authenticate;
using Funzone.Application.Commands.Users.RegisterUser;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Users
{
    using static TestFixture;

    public class UserTests : TestBase
    {
        [Test]
        public async Task RegisterByEmail_WithUniqueEmail_Successful()
        {
            var command = new RegisterUserByEmailCommand
            {
                Password = "123456",
                EmailAddress = "test@outlook.com"
            };

            await SendAsync(command);

            var authenticate = new AuthenticateCommand(command.EmailAddress, command.Password);
            var authenticatedResult = await SendAsync(authenticate);

            authenticatedResult.IsAuthenticated.ShouldBeTrue();
            authenticatedResult.User.EmailAddress.ShouldBe(command.EmailAddress);
        }

        [Test]
        public async Task Authenticate_IncorrectLogin_AuthenticateFailed()
        {
            var command = new AuthenticateCommand("test@outlook.com", "123456");

            var authenticateResult = await SendAsync(command);
            authenticateResult.IsAuthenticated.ShouldBe(false);
            authenticateResult.User.ShouldBeNull();
            authenticateResult.AuthenticationError.ShouldBe("Incorrect email or password");
        }
    }
}