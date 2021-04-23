namespace Funzone.Application.Commands.Users
{
    public class RegisterUserByEmailCommand : ICommand<bool>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}