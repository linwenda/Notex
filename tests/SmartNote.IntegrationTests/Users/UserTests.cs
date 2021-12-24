using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartNote.Application.Users.Commands;

namespace SmartNote.IntegrationTests.Users
{
    using static TestFixture;

    public class UserTests : TestBase
    {
        [Test]
        public async Task ShouldRegisterUser()
        {
            var registerCommand = new RegisterUserCommand
            {
                Email = "test@outlook.com",
                Password = "123456",
                FirstName = "BRUCE",
                LastName = "Lin"
            };

            await Send(registerCommand);

            var authenticationResult = await Send(new AuthenticateCommand(
                registerCommand.Email, registerCommand.Password));

            authenticationResult.IsAuthenticated.ShouldBeTrue();
            authenticationResult.User.Email.ShouldBe(registerCommand.Email);
        }
    }
}