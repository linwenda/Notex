using System.Threading.Tasks;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Application.Users.Command;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;
using MarchNote.Domain.Users;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace MarchNote.IntegrationTests.Users
{
    using static TestFixture;

    [Story(AsA = "As an account holder",
        IWant = "I want to change password",
        SoThat = "So that i can use new password login")]
    public class ChangePasswordTest : TestBase
    {
        private UserId _currentUserId;
        private const string Login = "test@outlook.com";
        private const string OldPassword = "123456";
        private const string NewPassword = "654321";

        private async Task GivenUserAccount()
        {
            var response = await Send(new RegisterUserCommand
            {
                Email = Login,
                Password = OldPassword
            });

            response.Code.ShouldBe(DefaultResponseCode.Succeeded);

            _currentUserId = new UserId(response.Data);
        }

        private async Task WhenThePasswordChanged()
        {
            var changeCommand = new ChangePasswordCommand
            {
                NewPassword = NewPassword,
                OldPassword = OldPassword
            };

            var response = await SendAsUser(changeCommand, _currentUserId);

            response.Code.ShouldBe(DefaultResponseCode.Succeeded);
        }

        private async Task ThenTheOldPasswordShouldAuthenticateFailed()
        {
            var authenticateResponse = await Send(new AuthenticateCommand(Login, OldPassword));
            authenticateResponse.Code.ShouldBe((int) ExceptionCode.UserPasswordIncorrect);
        }

        private async Task AndTheNewPasswordShouldAuthenticateSucceeded()
        {
            var authenticateResponse = await Send(new AuthenticateCommand(Login, NewPassword));
            authenticateResponse.Code.ShouldBe(DefaultResponseCode.Succeeded);
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}