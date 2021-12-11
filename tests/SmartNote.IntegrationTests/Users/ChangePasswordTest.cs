using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using SmartNote.Core.Application.Users.Contracts;
using SmartNote.Core.Domain.Users.Exceptions;
using TestStack.BDDfy;

namespace SmartNote.IntegrationTests.Users
{
    using static TestFixture;

    [Story(AsA = "As an account holder",
        IWant = "I want to change password",
        SoThat = "So that i can use new password login")]
    public class ChangePasswordTest : TestBase
    {
        private Guid _currentUserId;
        private const string Login = "test@outlook.com";
        private const string OldPassword = "123456";
        private const string NewPassword = "654321";

        private async Task GivenUserAccount()
        {
            _currentUserId = await Send(new RegisterUserCommand
            {
                Email = Login,
                Password = OldPassword,
                FirstName = "BRUCE",
                LastName = "Lin"
            });
        }

        private async Task WhenThePasswordChanged()
        {
            var changeCommand = new ChangePasswordCommand
            {
                NewPassword = NewPassword,
                OldPassword = OldPassword
            };

             await SendAsUser(changeCommand, _currentUserId);
        }

        private async Task ThenTheOldPasswordShouldAuthenticateFailed()
        {
            var ex = await Should.ThrowAsync<IncorrectEmailOrPasswordException>(async () =>
                await Send(new AuthenticateCommand(Login, OldPassword)));

            ex.ShouldNotBeNull();
        }

        private async Task AndTheNewPasswordShouldAuthenticateSucceeded()
        {
            var response = await Send(new AuthenticateCommand(Login, NewPassword));
            response.Email.ShouldNotBeNullOrEmpty();
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}