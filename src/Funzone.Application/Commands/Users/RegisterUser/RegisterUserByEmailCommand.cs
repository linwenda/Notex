namespace Funzone.Application.Commands.Users.RegisterUser
{
    public class RegisterUserByEmailCommand : ICommand
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}