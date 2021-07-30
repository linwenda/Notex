using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Users.Command;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.Users
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
                Password = "123456"
            };

            await Send(registerCommand);

            var authenticateResponse = await Send(new AuthenticateCommand(
                registerCommand.Email,registerCommand.Password));

            authenticateResponse.Code.ShouldBe(DefaultResponseCode.Succeeded);
            authenticateResponse.Data.Email.ShouldBe(registerCommand.Email);
        }
    }
}