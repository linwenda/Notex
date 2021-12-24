using FluentValidation;
using MediatR;

namespace SmartNote.Application.Users.Commands;

//Specific command
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

public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticateCommandValidator()
    {
        RuleFor(v => v.Email).EmailAddress();
        RuleFor(v => v.Password).NotNull().Length(6, 50);
    }
}