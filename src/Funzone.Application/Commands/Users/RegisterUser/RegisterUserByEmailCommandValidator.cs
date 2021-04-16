using FluentValidation;

namespace Funzone.Application.Commands.Users.RegisterUser
{
    public class RegisterUserByEmailCommandValidator : AbstractValidator<RegisterUserByEmailCommand>
    {
        public RegisterUserByEmailCommandValidator()
        {
            RuleFor(c => c.EmailAddress).EmailAddress();
            RuleFor(c => c.Password).NotNull().Length(6, 50);
        }
    }
}