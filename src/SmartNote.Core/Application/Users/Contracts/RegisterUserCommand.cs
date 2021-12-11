using FluentValidation;

namespace SmartNote.Core.Application.Users.Contracts;

public class RegisterUserCommand : ICommand<Guid>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(r => r.Email).EmailAddress();
        RuleFor(v => v.LastName).MaximumLength(32);
        RuleFor(v => v.FirstName).MaximumLength(32);

        RuleFor(r => r.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(64);
    }
}