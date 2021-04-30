using MediatR;

namespace Funzone.Application.Users.Commands
{
    //It's a command, but nothing changes
    public class AuthenticateCommand : IRequest<AuthenticationResult>
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