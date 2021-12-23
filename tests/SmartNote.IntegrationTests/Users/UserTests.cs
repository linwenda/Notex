using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartNote.Core.Application.Users.Commands;

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

            var authenticateResponse = await Send(new AuthenticateCommand(
                registerCommand.Email, registerCommand.Password));

            authenticateResponse.Email.ShouldBe(registerCommand.Email);
        }
    }
}