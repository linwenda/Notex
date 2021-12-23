using FluentValidation;
using SmartNote.Core.Application.Users.Queries;

namespace SmartNote.Core.Application.Users.Commands;

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