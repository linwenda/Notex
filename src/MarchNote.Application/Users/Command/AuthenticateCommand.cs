using FluentValidation;
using MarchNote.Application.Configuration.Commands;

namespace MarchNote.Application.Users.Command
{
    public class AuthenticateCommand : ICommand<UserAuthenticateDto>
    {
        public AuthenticateCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
    
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(v => v.Email).EmailAddress();
            RuleFor(v => v.Password).NotNull().Length(6, 50);
        }
    }
}