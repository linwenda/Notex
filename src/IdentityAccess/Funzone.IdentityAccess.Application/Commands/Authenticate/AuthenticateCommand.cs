using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.IdentityAccess.Application.Commands.Authenticate
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