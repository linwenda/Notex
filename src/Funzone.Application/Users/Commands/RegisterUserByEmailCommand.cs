using Funzone.Application.Configuration.Commands;

namespace Funzone.Application.Users.Commands
{
    public class RegisterUserByEmailCommand : ICommand<bool>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}