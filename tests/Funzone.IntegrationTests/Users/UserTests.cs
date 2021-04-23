using System.Threading.Tasks;
using Funzone.Application.Commands.Users;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Funzone.IntegrationTests.Users
{
    using static TestFixture;

    public class UserTests : TestBase
    {
        [Test]
        public async Task ShouldRegisterUserByEmail()
        {
            await Run<IMediator>(async mediator =>
            {
                var command = new RegisterUserByEmailCommand
                {
                    Password = "123456",
                    EmailAddress = "test@outlook.com"
                };

                await mediator.Send(command);

                var authenticateCommand = new AuthenticateCommand(command.EmailAddress, command.Password);
                var authenticatedResult = await mediator.Send(authenticateCommand);

                authenticatedResult.IsAuthenticated.ShouldBeTrue();
                authenticatedResult.User.EmailAddress.ShouldBe(command.EmailAddress);
            });
        }

        [Test]
        public async Task ShouldAuthenticateFailedWhenIncorrectLogin()
        {
            await Run<IMediator>(async mediator =>
            {
                var command = new AuthenticateCommand("test@outlook.com", "123456");

                var authenticateResult = await mediator.Send(command);
                authenticateResult.IsAuthenticated.ShouldBe(false);
                authenticateResult.User.ShouldBeNull();
                authenticateResult.AuthenticationError.ShouldBe("Incorrect email or password");
            });
        }
    }
}