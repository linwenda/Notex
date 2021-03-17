using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.IdentityAccess.Application.Commands.Authenticate
{
    public class AuthenticateCommand : CommandBase<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}