using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.Services.Identity.Application.Commands.Authenticate
{
    public class AuthenticateCommand : CommandBase<AuthenticationResult>
    {
        public AuthenticateCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}