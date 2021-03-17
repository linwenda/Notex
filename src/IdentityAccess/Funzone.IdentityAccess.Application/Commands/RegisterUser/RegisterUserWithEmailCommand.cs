using Funzone.BuildingBlocks.Application.Commands;

namespace Funzone.IdentityAccess.Application.Commands.RegisterUser
{
    public class RegisterUserWithEmailCommand : CommandBase
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}