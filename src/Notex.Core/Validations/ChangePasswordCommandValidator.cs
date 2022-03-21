using FluentValidation;
using Notex.Messages.Users.Commands;

namespace Notex.Core.Validations;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(v => v.OldPassword).NotNull().Length(6, 50);
        RuleFor(v => v.NewPassword).NotNull().Length(6, 50);
    }
}