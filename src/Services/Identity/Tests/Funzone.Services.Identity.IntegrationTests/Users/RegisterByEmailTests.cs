using System.Threading.Tasks;
using Funzone.Services.Identity.Application.Commands.Authenticate;
using Funzone.Services.Identity.Application.Commands.RegisterUser;
using NUnit.Framework;
using Shouldly;

namespace Funzone.Services.Identity.IntegrationTests.Users
{
    using static TestFixture;

    public class RegisterByEmailTests : TestBase
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
    }
}